using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Framework.Utils
{
    public static class Geometry
    {
        private static Dictionary<string, List<Vector2>> _circleCache = new Dictionary<string, List<Vector2>>();
        
        /// <summary>
        /// Creates a list of vectors that represents a circle
        /// </summary>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <returns>A list of vectors that, if connected, will create a circle</returns>
        public static List<Vector2> CreateCircle(float radius, int sides)
        {
            // Look for a cached version of this circle
            var circleKey = radius + "x" + sides;
            if (_circleCache.ContainsKey(circleKey))
            {
                return _circleCache[circleKey];
            }

            var vectors = new List<Vector2>();
            var step = Mathf.TwoPI / sides;

            for (var theta = 0f; theta < Mathf.TwoPI; theta += step)
            {
                vectors.Add(new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta)));
            }


            vectors.Add(new Vector2(radius * Mathf.Cos(0), radius * Mathf.Sin(0)));
            _circleCache.Add(circleKey, vectors);

            return vectors;
        }

        /// <summary>
        /// Creates a list of vectors that represents an arc
        /// </summary>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate in the circle that this will cut out from</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The radians to draw, clockwise from the starting angle</param>
        /// <returns>A list of vectors that, if connected, will create an arc</returns>
        public static List<Vector2> CreateArc(float radius, int sides, float startingAngle, float radians)
        {
            var points = new List<Vector2>();
            points.AddRange(CreateCircle(radius, sides));
            points.RemoveAt(points.Count - 1);

            // The circle starts at (radius, 0)
            double curAngle = 0.0;
            double anglePerSide = MathHelper.TwoPi / sides;

            // "Rotate" to the starting point
            while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
            {
                curAngle += anglePerSide;

                // move the first point to the end
                points.Add(points[0]);
                points.RemoveAt(0);
            }

            // Add the first point, just in case we make a full circle
            points.Add(points[0]);

            // Now remove the points at the end of the circle to create the arc
            int sidesInArc = (int) ((radians / anglePerSide) + 0.5);
            points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

            return points;
        }
    }
}