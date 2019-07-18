using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Hevadea.Framework
{
    public static class Geometry
    {
        private static readonly Dictionary<string, List<Vector2>> CircleCache = new Dictionary<string, List<Vector2>>();

        /// <summary>
        /// Creates a list of vectors that represents a circle
        /// </summary>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <returns>A list of vectors that, if connected, will create a circle</returns>
        public static List<Vector2> Circle(float radius, int sides)
        {
            // Look for a cached version of this circle
            var circleKey = radius + "x" + sides;
            if (CircleCache.ContainsKey(circleKey)) return CircleCache[circleKey];

            var vectors = new List<Vector2>();
            var step = Mathf.TwoPI / sides;

            for (var theta = 0f; theta < Mathf.TwoPI; theta += step)
                vectors.Add(new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta)));

            vectors.Add(new Vector2(radius * Mathf.Cos(0), radius * Mathf.Sin(0)));
            CircleCache.Add(circleKey, vectors);

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
        public static List<Vector2> Arc(float radius, int sides, float startingAngle, float radians)
        {
            var points = new List<Vector2>();
            points.AddRange(Circle(radius, sides));
            points.RemoveAt(points.Count - 1);

            // The circle starts at (radius, 0)
            var curAngle = 0.0;
            double anglePerSide = MathHelper.TwoPi / sides;

            // "Rotate" to the starting point
            while (curAngle + anglePerSide / 2.0 < startingAngle)
            {
                curAngle += anglePerSide;

                // move the first point to the end
                points.Add(points[0]);
                points.RemoveAt(0);
            }

            // Add the first point, just in case we make a full circle
            points.Add(points[0]);

            // Now remove the points at the end of the circle to create the arc
            var sidesInArc = (int) (radians / anglePerSide + 0.5);
            points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

            return points;
        }

        public static void Line(Point p0, Point p1, Action<Point> action)
        {
            Line(p0.X, p0.Y, p1.X, p1.Y, action);
        }

        public static void Line(int x, int y, int x2, int y2, Action<Point> action)
        {
            var w = x2 - x;
            var h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1;
            else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1;
            else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1;
            else if (w > 0) dx2 = 1;
            var longest = Math.Abs(w);
            var shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1;
                else if (h > 0) dy2 = 1;
                dx2 = 0;
            }

            var numerator = longest >> 1;
            for (var i = 0; i <= longest; i++)
            {
                action(new Point(x, y));
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}