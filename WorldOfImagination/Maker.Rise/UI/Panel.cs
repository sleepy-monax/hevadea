using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI
{
    public class Panel : Control
    {
        public Color Color { get; set; } = Color.Transparent;
        
        public Panel()
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
