using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Rrs.Expandos
{
    public static class ExpandoExtensions
    {
        public static ExpandoObject Merge(this ExpandoObject left, ExpandoObject right)
        {
            var o = new ExpandoObject();
            var merged = (IDictionary<string, object>)o;

            var leftDictionary = (IDictionary<string, object>)left;
            var rightDictionary = (IDictionary<string, object>)right;

            if (leftDictionary.Keys.Intersect(rightDictionary.Keys).Any()) throw new ArgumentException(@"""right"" contains keys that are present in left", nameof(right));

            foreach (var p in leftDictionary.Concat(rightDictionary))
            {
                merged[p.Key] = p.Value;
            }

            return o;
        }

        public static ExpandoObject Except(this ExpandoObject o, params string[] exceptList)
        {
            var d = (IDictionary<string, object>)o;
            foreach(var except in exceptList)
            {
                d.Remove(except);
            }

            return o;
        }
    }
}
