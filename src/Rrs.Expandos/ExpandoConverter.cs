using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Rrs.Expandos
{
    static class ExpandoConverter
    {
        public static ExpandoObject Convert<T>(T obj)
        {
            return ExpandoConverter<T>.Convert(obj);
        }
    }

    static class ExpandoConverter<T>
    {
        private static readonly Func<T, ExpandoObject> _converter;

        static ExpandoConverter()
        {
            var exps = new List<Expression>();

            var obj = Expression.Parameter(typeof(T), "obj");

            var props = typeof(T).GetFlattenedProperties(BindingFlags.Instance | BindingFlags.Public);

            var expando = Expression.Variable(typeof(ExpandoObject), "expando");

            exps.Add(Expression.Assign(expando, Expression.New(typeof(ExpandoObject))));

            var addMeth = typeof(IDictionary<string, object>).GetMethod("Add", new[] { typeof(string), typeof(object) });

            foreach (var prop in props)
            {
                var key = Expression.Constant(prop.Name);
                var value = Expression.Property(obj, prop);

                var valueAsObj = Expression.Convert(value, typeof(object));
                exps.Add(Expression.Call(expando, addMeth, key, valueAsObj));
            }

            exps.Add(expando);

            var block = Expression.Block(new[] { expando }, exps);

            _converter = Expression.Lambda<Func<T, ExpandoObject>>(block, new[] { obj }).Compile();
        }

        public static ExpandoObject Convert(T obj)
        {
            return _converter(obj);
        }
    }
}
