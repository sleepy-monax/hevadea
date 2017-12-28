using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Ressource
{
    public class Sprite
    {
        private readonly SpriteSheet Sheet;
        private readonly int Index;
        private readonly Point SubSpriteSize;

        // Constructors -------------------------------------------------------

        public Sprite(SpriteSheet sheet, int index)
        {
            Sheet = sheet;
            Index = index;
            SubSpriteSize = new Point(8, 8);
        }

        public Sprite(SpriteSheet sheet, int index, Point subSpriteSize) : this(sheet, index)
        {
            SubSpriteSize = subSpriteSize;
        }

        // Methodes -----------------------------------------------------------

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Sheet.Texture, position, Sheet.GetTile(Index), color);
        }

        public void DrawSubSprite(SpriteBatch spriteBatch, Vector2 position, Point subSprite,Color color)
        {
            var me = Sheet.GetTile(Index);
            var subMe = new Rectangle(me.X + subSprite.X * SubSpriteSize.X,
                                      me.Y + subSprite.Y * SubSpriteSize.Y,
                                      
                                      SubSpriteSize.X,
                                      SubSpriteSize.Y);

            spriteBatch.Draw(Sheet.Texture, position, subMe, color);
        }
    }
}
