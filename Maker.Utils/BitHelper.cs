namespace Maker.Utils
{
    public static class BitHelper
    {

        public static byte WriteBit(this byte value, byte selectedBit, bool bit)
        {
            if (bit)
            {
                return (byte)(value | (1 << selectedBit));

            }
            else
            {
                return (byte)(value & ~(1 << selectedBit));
            }
        }

        public static int WriteBit(this int value, byte selectedBit, bool bit)
        {
            if (bit)
            {
                return (value | (1 << selectedBit));

            }
            else
            {
                return (value & ~(1 << selectedBit));
            }
        }

        public static bool ReadBit(this byte value, int selectedBit)
        {
            return (value & (1 << selectedBit)) != 0;
        }

        public static bool ReadBit(this int value, int selectedBit)
        {
            return (value & (1 << selectedBit)) != 0;
        }

    }
}
