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
        public int WorldX => X * Constant.TileSize;
        public int WorldY => Y * Constant.TileSize;

        public Point ToOnScreenPosition()
        {
            return new Point(X * Constant.TileSize, Y * Constant.TileSize);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(WorldX + 8, WorldY + 8);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(ToOnScreenPosition(), new Point(Constant.TileSize, Constant.TileSize));
        }

        public bool IsColliding(Entity e, int width, int height)
        {
            return Colision.Check(X * Constant.TileSize,
                Y * Constant.TileSize,
                Constant.TileSize, Constant.TileSize,
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
            return Colision.Check(X * Constant.TileSize,
                Y * Constant.TileSize,
                Constant.TileSize, Constant.TileSize,
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
            return Equals((TilePosition) obj);
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