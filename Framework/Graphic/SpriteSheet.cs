using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class SpriteSheet
    {
        private readonly Point TileCount;

        public readonly Texture2D Texture;
        public readonly Point TileSize;

        public SpriteSheet(Texture2D texture, Point tileSize)
        {
            if (Rise.NoGraphic) return;

            Texture = texture;
            TileSize = tileSize;
            TileCount.X = Texture.Width / TileSize.X;
            TileCount.Y = Texture.Height / TileSize.Y;
        }

        public Rectangle GetTile(int Index)
        {
            if (Rise.NoGraphic) return Rectangle.Empty;

            var pos = new Point(Index % TileCount.X, Index / TileCount.X);
            return new Rectangle(pos.X * TileSize.X, pos.Y * TileSize.Y, TileSize.X, TileSize.Y);
        }

        internal Rectangle GetTile(Point position)
        {
            if (Rise.NoGraphic) return Rectangle.Empty;

            return new Rectangle(position.X * TileSize.X, position.Y * TileSize.Y, TileSize.X, TileSize.Y);
        }

        public void Draw(SpriteBatch spriteBatch, int index, Rectangle destination, Color color)
        {
            spriteBatch.Draw(Texture, destination, GetTile(index), color);
        }
    }
}