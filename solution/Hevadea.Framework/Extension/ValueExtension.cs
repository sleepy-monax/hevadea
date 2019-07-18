using System;

namespace Hevadea.Framework.Extension
{
    public static class ValueExtension
    {
        public static byte WriteBit(this byte value, byte selectedBit, bool bit)
        {
            if (bit) return (byte) (value | (1 << selectedBit));

            return (byte) (value & ~(1 << selectedBit));
        }

        public static int WriteBit(this int value, byte selectedBit, bool bit)
        {
            if (bit) return value | (1 << selectedBit);

            return value & ~(1 << selectedBit);
        }

        public static bool ReadBit(this byte value, int selectedBit)
        {
            return (value & (1 << selectedBit)) != 0;
        }

        public static bool ReadBit(this int value, int selectedBit)
        {
            return (value & (1 << selectedBit)) != 0;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}