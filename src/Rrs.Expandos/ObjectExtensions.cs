using System.Dynamic;

namespace Rrs.Expandos
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ToExpando<T>(this T obj)
        {
            if (obj is ExpandoObject e) return e;
            return ExpandoConverter.Convert(obj);
        }

        public static ExpandoObject ToExpando<T>(this T obj, params string[] exceptList)
        {
            return ExpandoConverter.Convert(obj).Except(exceptList);
        }

        public static ExpandoObject Merge<T1, T2>(this T1 left, T2 right)
        {
            return left.ToExpando().Merge(right.ToExpando());
        }
    }
}
