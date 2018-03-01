using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets
{
    public class BlurPanel : Panel
    {
        public Color Color { get; set; } = Color.Black * 0.75f;
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Host, Color);
         
            spriteBatch.DrawRectangle(Host, Color.White * 0.05f);
            
            base.Draw(spriteBatch, gameTime);
        }
    }
}