using System;

namespace WorldOfImagination.Utils
{
    public static class MathUtils
    {
        public static float Interpolate(float value)
        {
            return (float)((Math.Sin(value * Math.PI - Math.PI / 2f) + 1f) / 2f);

            //(sin(x * PI - PI / 2) + 1) / 2
            
            //return (float)((Math.Sin(value * Math.PI - Math.PI / 2f) + 1f) / 2f);
        }
    }
}