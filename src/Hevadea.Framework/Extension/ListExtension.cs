using System.Collections.Generic;

namespace Hevadea.Framework.Extension
{
    public static class ListExtension
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

        public static T Pop<T>(this List<T> list)
        {
            T r = list[0];
            list.RemoveAt(0);
            return r;
        }

        public static void Push<T>(this List<T> list, T item)
        {
            list.Insert(0, item);
        }
    }
}
