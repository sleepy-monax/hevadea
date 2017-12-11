using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent.Ressource
{
    public class TileSheet
    {
        public readonly Texture2D Texture;
        public readonly Point TileSize;

        private readonly Point TileCount;

        public TileSheet(Texture2D texture, Point tileSize)
        {
            Texture = texture;
            TileSize = tileSize;
            TileCount.X = Texture.Width / TileSize.X;
            TileCount.Y = Texture.Height / TileSize.Y;
        }

        public Rectangle GetTile(int Index)
        {
            var pos = new Point(Index % TileCount.X, Index / TileCount.Y );
            return new Rectangle(pos.X * TileSize.X, pos.Y * TileSize.Y, TileSize.X, TileSize.Y);
        }
    }
}