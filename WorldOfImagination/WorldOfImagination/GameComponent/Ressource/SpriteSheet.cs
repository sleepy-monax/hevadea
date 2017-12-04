using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent.Ressource
{
    public class SpriteSheet
    {

        public Texture2D Texture    { get; private set; }
        public int CellWidth        { get; private set; }
        public int CellHeight       { get; private set; }
        
        public SpriteSheet(Texture2D texture, int cellWidth, int cellHeight)
        {
            Texture = texture;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
        }

        public Rectangle GetSprite(int index)
        {
            Point cell = new Point(Texture.Width / CellWidth, Texture.Height / CellHeight);
            return new Rectangle(index % cell.X, index / cell.X, CellWidth, CellHeight);
        }

    }
}
