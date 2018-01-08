using Maker.Rise.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise.Extension;

namespace Maker.Rise.UI
{
    public class Label : Control
    {
        public string Text { get; set; } = "Label";
        public SpriteFont Font { get; set; }

        public Label()
        {
            Font = EngineRessources.FontBebas;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Font, Text, Bound, Alignement.Center, TextStyle.DropShadow, Color.White);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}