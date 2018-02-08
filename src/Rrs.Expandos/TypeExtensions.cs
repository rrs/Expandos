using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rrs.Expandos
{
    static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetFlattenedProperties(this Type type, BindingFlags bindingFlags = BindingFlags.Default)
        {
            if (!type.IsInterface)
                return type.GetProperties(bindingFlags);

            return (new Type[] { type })
                   .Concat(type.GetInterfaces())
                   .SelectMany(i => i.GetProperties(bindingFlags));
        }
    }
}
