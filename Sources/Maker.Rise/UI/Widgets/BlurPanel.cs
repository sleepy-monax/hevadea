using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class BlurPanel : Panel
    {
        public Color Color { get; set; } = Color.Black * 0.75f;
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Engine.Scene.BluredScene, Host, Host, Color.White);
            spriteBatch.FillRectangle(Host, Color);
         
            spriteBatch.DrawRectangle(Host, Color.White * 0.05f);
            
            base.Draw(spriteBatch, gameTime);
        }
    }
}