using System;
using Hevadea.Framework.Extension;

namespace Hevadea.Framework.Utils
{
    public static class RandomUtils
    {
		public static T Choose<T>(params T[] values)
		{
			return values[new Random().Next(values.Length)];
		}
    }
}
