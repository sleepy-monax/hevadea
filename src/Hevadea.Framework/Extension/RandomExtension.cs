using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Framework.Extension
{
    public static class RandomExtension
    {
        public static Vector2 NextVector2(this Random random, int minX, int maxX, int minY, int maxY)
        {
            return new Vector2(random.Next(minX, maxX), random.Next(minY, maxY));
        }

        public static Vector2 NextVector2(this Random random, int min, int max)
        {
            return new Vector2(random.Next(min, max), random.Next(min, max));
        }

        public static float NextFloat(this Random rnd, float max)
            => (float)rnd.NextDouble() * max;

        public static float NextFloat(this Random rnd)
            => (float)rnd.NextDouble();

        public static float NextFloatRange(this Random rnd, float max)
            => (rnd.NextFloat() - 0.5f) * 2f * max;

        public static float NextFloatRange(this Random rnd)
            => (rnd.NextFloat() - 0.5f) * 2f;

        public static T NextValue<T>(this Random rnd, params T[] values)
            => values[rnd.Next(values.Length)];

        public static void Shuffle<T>(this Random rng, T[] array)
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