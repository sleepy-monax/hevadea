using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic.SpriteAtlas
{
    public class Sprite
    {
        private readonly SpriteSheet Sheet;
        private readonly Rectangle Source;
        private readonly Point SubSpriteSize;

        // Constructors -------------------------------------------------------

        public Sprite(SpriteSheet sheet, int index)
        {
            Sheet = sheet;
            Source = sheet.GetTile(index);
            SubSpriteSize = new Point(8, 8);
        }

        public Sprite(SpriteSheet sheet, Point position)
        {
            Sheet = sheet;
            Source = sheet.GetTile(position);
            SubSpriteSize = new Point(8, 8);
        }

        public Sprite(SpriteSheet sheet, Point position, Point size, Point? subSpriteSize = null)
        {
            Sheet = sheet;
            Source = new Rectangle(position.X * Sheet.TileSize.X ,position.Y * Sheet.TileSize.Y, size.X* Sheet.TileSize.X, size.Y * Sheet.TileSize.Y);
            SubSpriteSize = subSpriteSize?? new Point(8,8);
        }

        public Sprite(SpriteSheet sheet, int index, Point subSpriteSize) : this(sheet, index)
        {
            SubSpriteSize = subSpriteSize;
        }

        // Methodes -----------------------------------------------------------

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, position, Source, color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, position, Source, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, destination, Source, color);
        }

        public void DrawSubSprite(SpriteBatch spriteBatch, Vector2 position, Point subSprite, Color color)
        {
            var subMe = new Rectangle(Source.X + subSprite.X * SubSpriteSize.X,
                Source.Y + subSprite.Y * SubSpriteSize.Y,
                SubSpriteSize.X,
                SubSpriteSize.Y);

            spriteBatch.Draw(Sheet.Texture, position, subMe, color);
        }
    }
}