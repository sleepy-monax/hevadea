using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Extension
{
    public static class LinqExtension
    {

        public static IEnumerable<Tvalue> ForEarch<Tvalue>(this IEnumerable<Tvalue> collection, Action<Tvalue> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }
    }
}
