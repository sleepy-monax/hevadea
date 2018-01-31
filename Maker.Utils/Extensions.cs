using System;
using System.Collections.Generic;
using System.Linq;

namespace Maker.Utils
{
    public static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone)
        {
            var result = new List<T>();

            foreach (var item in listToClone)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
