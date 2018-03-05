using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets
{
    public class BackButton : Widget
    {
        public bool EnableBorder { get; set; } = false;
        public Color OverColor { get; set; } = Color.Gold;
        public Color IdleColor { get; set; } = Color.White;
        public Color TextColor { get; set; } = Color.White;
        public SpriteFont Font { get; set; } = Rise.Ui.DefaultFont;
        private EasingManager _easing = new EasingManager { Speed = 2.5f };
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _easing.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;
            _easing.Update(gameTime.ElapsedGameTime.TotalSeconds);
            
            

            var bounce = (int) (Scale(24) * _easing.GetValue(EasingFunctions.ElasticEaseOut, EasingFunctions.ElasticEaseIn));
            var bounceW = Host.Width + bounce;
            
            var rect = new Rectangle(Host.X,Host.Y, 
                                     bounceW, Host.Height);

            if (EnableBorder)
            {
                spriteBatch.FillRectangle(rect, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));
                spriteBatch.DrawRectangle(rect, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));
            }
            
            spriteBatch.FillRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));
            
            }
    }
}