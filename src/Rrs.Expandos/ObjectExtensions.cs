using System.Dynamic;

namespace Rrs.Expandos
{
    static class ObjectExtensions
    {
        public static ExpandoObject ToExpando<T>(this T obj)
        {
            return ExpandoConverter.Convert(obj);
        }
    }
}
