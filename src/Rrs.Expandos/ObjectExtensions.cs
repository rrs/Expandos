using System.Dynamic;

namespace Rrs.Expandos
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ToExpando<T>(this T obj)
        {
            return ExpandoConverter.Convert(obj);
        }
    }
}
