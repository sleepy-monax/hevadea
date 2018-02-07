// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local

using Maker.Rise.Extension;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class Button : Widget
    {
        public bool BlurBackground { get; set; } = true;
        public Color OverColor { get; set; } = Color.Gold;
        public Color IdleColor { get; set; } = Color.White;
        public Color TextColor { get; set; } = Color.White;
        public SpriteFont Font { get; set; } = EngineRessources.FontBebas;
        public string Text { get; set; } = "Button";
        private EasingManager _easing = new EasingManager { Speed = 0.5f };

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _easing.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;
            _easing.Update(gameTime);
            
            if (BlurBackground)
            {
                spriteBatch.Draw(Engine.Scene.BluredScene, Host, Host, Color.White * _easing.GetValue(EasingFunctions.Linear));
            }
            
            spriteBatch.FillRectangle(Host, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(Host, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));

            var bounceW = (int) (Host.Width *  _easing.GetValue(EasingFunctions.QuadraticEaseInOut));
            var bounceH = (int) (Host.Height * _easing.GetValue(EasingFunctions.QuadraticEaseInOut));
            
            var rect = new Rectangle(Host.X + Host.Width / 2 - bounceW / 2,
                Host.Y + Host.Height / 2 - bounceH / 2, 
                                     bounceW, bounceH);
            
            spriteBatch.FillRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));

            var texSize = Font.MeasureString(Text);
            spriteBatch.DrawString(Font, Text, Bound.Center.ToVector2() - (new Vector2(texSize.X / 2, (texSize.Y / 2) - 4f)), Color.Black * 0.1f);
            spriteBatch.DrawString(Font, Text, Bound.Center.ToVector2() - (new Vector2(texSize.X / 2, texSize.Y / 2)), TextColor);
        }

    }
}
