using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Tiles
{
    public class TilePosition
    {
        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int WorldX => X * GLOBAL.Unit;
        public int WorldY => Y * GLOBAL.Unit;

        public Point ToOnScreenPosition()
        {
            return new Point(X * GLOBAL.Unit, Y * GLOBAL.Unit);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(WorldX + 8, WorldY + 8);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(ToOnScreenPosition(), new Point(GLOBAL.Unit, GLOBAL.Unit));
        }

        public bool IsColliding(Entity e, int width, int height)
        {
            return Colision.Check(X * GLOBAL.Unit,
                Y * GLOBAL.Unit,
                GLOBAL.Unit, GLOBAL.Unit,
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
            return Colision.Check(X * GLOBAL.Unit,
                Y * GLOBAL.Unit,
                GLOBAL.Unit, GLOBAL.Unit,
                x,
                y,
                width, height);
        }

        public static bool operator !=(TilePosition left, TilePosition right)
        {
            return !(left == right);
        }

        public static bool operator ==(TilePosition left, TilePosition right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        protected bool Equals(TilePosition other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TilePosition)obj);
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
    }
}