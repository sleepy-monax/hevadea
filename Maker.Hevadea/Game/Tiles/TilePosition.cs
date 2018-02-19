using Maker.Hevadea.Game.Entities;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Tiles
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
        public int WorldX => X * ConstVal.TileSize;
        public int WorldY => Y * ConstVal.TileSize;

        public Point ToOnScreenPosition()
        {
            return new Point(X * ConstVal.TileSize, Y * ConstVal.TileSize);
        }

        public bool IsColiding(Entity e, int width, int height)
        {
            return Colision.Check(X * ConstVal.TileSize,
                Y * ConstVal.TileSize,
                ConstVal.TileSize, ConstVal.TileSize,
                e.X,
                e.Y,
                width, height);
        }

        public bool IsColiding(float x, float y, int width, int height)
        {
            return Colision.Check(X * ConstVal.TileSize,
                Y * ConstVal.TileSize,
                ConstVal.TileSize, ConstVal.TileSize,
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
            return (left.X == right.X) | (left.Y == right.Y);
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
    }
}