using System;
using System.Collections.Generic;
using System.Linq;

namespace Maker.Rise.Logic.Scripting.Compiler
{
    public static class Utils
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static string PrepareString(string str)
        {
            return str.Replace("\r\n", "").Replace("\n", ""); // Remove new line char.
        }
    }
}