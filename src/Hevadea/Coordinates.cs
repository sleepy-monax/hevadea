using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities;
using Hevadea.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System;

namespace Hevadea.GameObjects.Tiles
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int WorldX => X * Game.Unit;
        public int WorldY => Y * Game.Unit;

        public Point ToOnScreenPosition()
        {
            return new Point(X * Game.Unit, Y * Game.Unit);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(WorldX + 8, WorldY + 8);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(ToOnScreenPosition(), new Point(Game.Unit, Game.Unit));
        }

        public bool IsColliding(Entity e, int width, int height)
        {
            return ColisionUtils.Colinding(X * Game.Unit,
                Y * Game.Unit,
                Game.Unit, Game.Unit,
                e.X,
                e.Y,
                width, height);
        }

        public bool IsColliding(Rectangle rect)
        {
            return IsColliding(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public bool IsColliding(float x, float y, int width, int height)
        {
            return ColisionUtils.Colinding(X * Game.Unit,
                Y * Game.Unit,
                Game.Unit, Game.Unit,
                x,
                y,
                width, height);
        }

        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !(left == right);
        }

        public static bool operator ==(Coordinates left, Coordinates right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        protected bool Equals(Coordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Coordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public PathFinder.Node AsNode()
        {
            return new PathFinder.Node(X, Y);
        }

        public bool InLevelBound(Level level)
        {
            return !(X < 0 || Y < 0 || X >= level.Width || Y >= level.Height);
        }
    }
}