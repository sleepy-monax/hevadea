using Maker.Rise.Extension;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class BackButton : Widget
    {
        public bool BlurBackground { get; set; } = true;
        public bool EnableBorder { get; set; } = false;
        public Color OverColor { get; set; } = Color.Gold;
        public Color IdleColor { get; set; } = Color.White;
        public Color TextColor { get; set; } = Color.White;
        public SpriteFont Font { get; set; } = EngineRessources.FontBebas;
        private EasingManager _easing = new EasingManager { Speed = 0.25f };

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _easing.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;
            _easing.Update(gameTime);
            
            

            var bounce = (int) (24 * _easing.GetValue(EasingFunctions.ElasticEaseOut, EasingFunctions.ElasticEaseIn));
            var bounceW = (int) (Host.Width + bounce);
            
            var rect = new Rectangle(Host.X,Host.Y, 
                                     bounceW, Host.Height);
            
            if (BlurBackground)
            {
                spriteBatch.Draw(Engine.Scene.BluredScene, rect, rect, Color.White * _easing.GetValue(EasingFunctions.Linear));
            }

            if (EnableBorder)
            {
                spriteBatch.FillRectangle(rect, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));
                spriteBatch.DrawRectangle(rect, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));
            }
            
            spriteBatch.FillRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));
            
            spriteBatch.Draw(EngineRessources.IconBack, new Rectangle(Host.X + bounce / 2, Host.Y, Host.Height, Host.Height), Color.White);
        }
    }
}