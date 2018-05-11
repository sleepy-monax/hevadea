using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets
{
    public class PictureWidget : Widget
    {
        public Texture2D Picture { get; set; } = null;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Picture, (Host.Center - Picture.Bounds.Center).ToVector2(), Color.White);
        }
    }
}