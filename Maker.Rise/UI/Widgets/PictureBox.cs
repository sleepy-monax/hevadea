using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class PictureBox : Control
    {
        public Texture2D Picture { get; set; } = EngineRessources.IconClose;

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Picture, (Bound.Center - Picture.Bounds.Center).ToVector2(), Color.White);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}