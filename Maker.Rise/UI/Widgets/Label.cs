using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
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
            spriteBatch.DrawString(Font, Text, new Rectangle(Bound.Location + new Point(0, 4), Bound.Size), Alignement.Center, TextStyle.DropShadow, Color.White);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}