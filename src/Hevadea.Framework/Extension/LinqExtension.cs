using System;
using System.Collections.Generic;

namespace Hevadea.Framework.Extension
{
    public static class LinqExtension
    {
        public static IEnumerable<T> ForEarch<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }
    }
}