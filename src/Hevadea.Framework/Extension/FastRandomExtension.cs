using Hevadea.Framework.Utils;

namespace Hevadea.Framework.Extension
{
    public static class FastRandomExtension
    {
        public static float NextFloat(this FastRandom rnd, float max)
        {
            return (float)rnd.NextDouble() * max;
        }

        public static float NextFloat(this FastRandom rnd)
        {
            return (float)rnd.NextDouble();
        }

        public static float NextFloatRange(this FastRandom rnd, float max)
        {
            return (rnd.NextFloat() - 0.5f) * 2f * max;
        }

        public static float NextFloatRange(this FastRandom rnd)
        {
            return (rnd.NextFloat() - 0.5f) * 2f;
        }

        public static T NextValue<T>(this FastRandom rnd, params T[] values)
        {
            return values[rnd.Next(values.Length)];
        }

        public static void Shuffle<T>(this FastRandom rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}