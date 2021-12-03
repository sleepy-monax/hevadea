using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class Sprite
    {
        private readonly SpriteSheet Sheet;
        private readonly Point SubSpriteSize;
        public Rectangle Bound { get; private set; }
        public Vector2 Size => Bound.Size.ToVector2();

        public Sprite(SpriteSheet sheet, int index)
        {
            Sheet = sheet;
            Bound = sheet.GetTile(index);
            SubSpriteSize = new Point(8, 8);
        }

        public Sprite(SpriteSheet sheet, Point position)
        {
            Sheet = sheet;
            Bound = sheet.GetTile(position);
            SubSpriteSize = new Point(8, 8);
        }

        public Sprite(SpriteSheet sheet, Point position, Point size, Point? subSpriteSize = null)
        {
            Sheet = sheet;
            Bound = new Rectangle(position.X * Sheet.TileSize.X, position.Y * Sheet.TileSize.Y,
                size.X * Sheet.TileSize.X, size.Y * Sheet.TileSize.Y);
            SubSpriteSize = subSpriteSize ?? new Point(8, 8);
        }

        public Sprite(SpriteSheet sheet, int index, Point subSpriteSize) : this(sheet, index)
        {
            SubSpriteSize = subSpriteSize;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, position, Bound, color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, position, Bound, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, position, Bound, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, destination, Bound, color);
        }

        public void DrawSubSprite(SpriteBatch spriteBatch, Vector2 position, Point subSprite, Color color)
        {
            var subMe = new Rectangle(Bound.X + subSprite.X * SubSpriteSize.X,
                Bound.Y + subSprite.Y * SubSpriteSize.Y,
                SubSpriteSize.X,
                SubSpriteSize.Y);

            spriteBatch.Draw(Sheet.Texture, position, subMe, color);
        }
    }
}