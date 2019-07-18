using Hevadea.Framework.Extension;
using System;

namespace Hevadea.Framework
{
    public class Noise
    {
        private readonly int[] _permutation;

        public Noise(int seed)
        {
            _permutation = new int[512];
            var permutation = new int[256];

            for (var i = 0; i < 256; i++) permutation[i] = i;

            new Random(seed).Shuffle(permutation);

            for (var x = 0; x < 512; x++) _permutation[x] = permutation[x % 256];
        }

        public double Generate(double x, double y, double z, int octaves, double persistence)
        {
            double total = 0;
            double frequency = 1;
            double amplitude = 1;
            for (var i = 0; i < octaves; i++)
            {
                total += Generate(x * frequency, y * frequency, z * frequency) * amplitude;

                amplitude *= persistence;
                frequency *= 2;
            }

            return total;
        }

        public double Generate(double x, double y, double z)
        {
            var xi = (int) x & 255; // Calculate the "unit cube" that the point asked will be located in
            var yi = (int) y & 255; // The left bound is ( |_x_|,|_y_|,|_z_| ) and the right bound is that
            var zi = (int) z & 255; // plus 1.  Next we calculate the location (from 0.0 to 1.0) in that cube.
            var xf = x - (int) x; // We also fade the location to smooth the result.
            var yf = y - (int) y;
            var zf = z - (int) z;
            var u = Fade(xf);
            var v = Fade(yf);
            var w = Fade(zf);

            var a = _permutation[xi] + yi; // This here is Perlin's hash function.  We take our x value (remember,
            var aa = _permutation[a] +
                     zi; // between 0 and 255) and get a random value (from our p[] array above) between
            var ab = _permutation[a + 1] +
                     zi; // 0 and 255.  We then add y to it and plug that into p[], and add z to that.
            var b = _permutation[xi + 1] +
                    yi; // Then, we get another random value by adding 1 to that and putting it into p[]
            var ba = _permutation[b] +
                     zi; // and add z to it.  We do the whole thing over again starting with x+1.  Later
            var
                bb = _permutation[b + 1] +
                     zi; // we plug aa, ab, ba, and bb back into p[] along with their +1's to get another set.
            // in the end we have 8 values between 0 and 255 - one for each vertex on the unit cube.
            // These are all interpolated together using u, v, and w below.

            double x1, x2, y1, y2;
            x1 = Lerp(
                Grad(_permutation[aa], xf, yf,
                    zf), // This is where the "magic" happens.  We calculate a new set of p[] values and use that to get
                Grad(_permutation[ba], xf - 1, yf,
                    zf), // our final gradient values.  Then, we interpolate between those gradients with the u value to get
                u); // 4 x-values.  Next, we interpolate between the 4 x-values with v to get 2 y-values.  Finally,
            x2 = Lerp(Grad(_permutation[ab], xf, yf - 1, zf), // we interpolate between the y-values to get a z-value.
                Grad(_permutation[bb], xf - 1, yf - 1, zf),
                u); // When calculating the p[] values, remember that above, p[a+1] expands to p[xi]+yi+1 -- so you are
            y1 = Lerp(x1, x2,
                v); // essentially adding 1 to yi.  Likewise, p[ab+1] expands to p[p[xi]+yi+1]+zi+1] -- so you are adding
            // to zi.  The other 3 parameters are your possible return values (see grad()), which are actually
            x1 = Lerp(
                Grad(_permutation[aa + 1], xf, yf,
                    zf - 1), // the vectors from the edges of the unit cube to the point in the unit cube itself.
                Grad(_permutation[ba + 1], xf - 1, yf, zf - 1),
                u);
            x2 = Lerp(Grad(_permutation[ab + 1], xf, yf - 1, zf - 1),
                Grad(_permutation[bb + 1], xf - 1, yf - 1, zf - 1),
                u);
            y2 = Lerp(x1, x2, v);

            return
                (Lerp(y1, y2, w) + 1) /
                2; // For convenience we bound it to 0 - 1 (theoretical min/max before is -1 - 1)
        }

        public double Grad(int hash, double x, double y, double z)
        {
            var h = hash & 15; // Take the hashed value and take the first 4 bits of it (15 == 0b1111)
            var
                u = h < 8 /* 0b1000 */
                    ? x
                    : y; // If the most signifigant bit (MSB) of the hash is 0 then set u = x.  Otherwise y.

            double v; // In Ken Perlin's original implementation this was another conditional operator (?:).  I
            // expanded it for readability.

            if (h < 4 /* 0b0100 */) // If the first and second signifigant bits are 0 set v = y
                v = y;
            else if (h == 12 /* 0b1100 */ || h == 14 /* 0b1110*/
            ) // If the first and second signifigant bits are 1 set v = x
                v = x;
            else // If the first and second signifigant bits are not equal (0/1, 1/0) set v = z
                v = z;

            return
                ((h & 1) == 0 ? u : -u) +
                ((h & 2) == 0
                    ? v
                    : -v
                ); // Use the last 2 bits to decide if u and v are positive or negative.  Then return their addition.
        }

        public double Fade(double t)
        {
            // Fade function as defined by Ken Perlin.  This eases coordinate values
            // so that they will "ease" towards integral values.  This ends up smoothing
            // the final output.
            return t * t * t * (t * (t * 6 - 15) + 10); // 6t^5 - 15t^4 + 10t^3
        }

        public double Lerp(double a, double b, double x)
        {
            return a + x * (b - a);
        }
    }
}