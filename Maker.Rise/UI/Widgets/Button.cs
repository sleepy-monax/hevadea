using System;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class Button : Control
    {
        public string Text { get; set; } = "Button";
        public Texture2D Icon { get; set; } = null;
        public bool EnableBlur { get; set; } = false;

        private FadingAnimation _animation = new FadingAnimation();
        private FadingAnimation _clickAnimation = new FadingAnimation();
        private Point _mouseClickPosition = Point.Zero;

        public Button()
        {
            _animation.Speed = 0.5f;
            _clickAnimation.Speed = 0.1f;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animation.Update(gameTime);
            _clickAnimation.Update(gameTime);

            var invClickanim = (1f - _clickAnimation.GetValue(EasingFunctions.Linear));
            
            var width = (int) (Host.Width * _animation.GetValue(EasingFunctions.Linear));
            var height = (int) (Host.Height * _animation.GetValue(EasingFunctions.Linear));

            var rectX = Host.X + Host.Width / 2 - width / 2;
            var rectY = Host.Y + Host.Height / 2 - height / 2;
            var rect = new Rectangle(rectX, rectY, width, height);

            var clickRectWidth =  (int)(width * _clickAnimation.GetValue(EasingFunctions.BounceEaseOut));
            var clickRectHeight = (int)(height* _clickAnimation.GetValue(EasingFunctions.BounceEaseOut));

            var clickRect = new Rectangle(rectX + (int)Math.Min(Math.Max(_mouseClickPosition.X - clickRectWidth / 2, 0f), width - clickRectWidth),
                                          rectY + (int)Math.Min(Math.Max(_mouseClickPosition.Y - clickRectHeight / 2, 0f), height - clickRectHeight),
                
                                          clickRectWidth, clickRectHeight);

            //spriteBatch.FillRectangle(new Rectangle(rectX + 4, rectY + 4, width, height), Color.Black * 0.25f * animation.GetValue(EasingFunctions.Linear));

            if (EnableBlur) spriteBatch.Draw(Engine.Scene.BlurRT, Host, Host, Color.White * _animation.GetValue(EasingFunctions.Linear));
            spriteBatch.FillRectangle(Host, Color.Gold * 0.5f * _animation.GetValue(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(Host, Color.White * 0.05f);
            spriteBatch.FillRectangle(Host, Color.White * 0.05f);

            
            //spriteBatch.FillRectangle(clickRect, Color.White * invClickanim * 0.75f);
            //spriteBatch.DrawRectangle(rect, Color.Gold * 0.5f * _animation.GetValue(EasingFunctions.Linear));
            

            if (Icon != null)
            {
                var iconY = (Bound.Height / 2 - Icon.Height / 2);

                spriteBatch.Draw(Icon, new Vector2(Bound.X + iconY, Bound.Y + iconY), Color.White);
            }

                
                spriteBatch.DrawString(EngineRessources.FontBebas, Text, Bound , Alignement.Center, TextStyle.Regular,
                    Color.White);
        }

        public override void OnMouseEnter()
        {
            Engine.Audio.PlaySoundEffect(EngineRessources.MenuPick);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            _animation.Show = GetMouseState() == MouseState.Over || GetMouseState() == MouseState.Down;

            if (Engine.Input.MouseLeftClick && Bound.Contains(Engine.Input.MousePosition))
            {
                _clickAnimation.Reset();
                _clickAnimation.Show = true;
                _mouseClickPosition = new Point(Engine.Input.MousePosition.X - Bound.X, Engine.Input.MousePosition.Y - Bound.Y);
                Engine.Audio.PlaySoundEffect(EngineRessources.MenuSelect);
            }

            if (_clickAnimation.GetValue(EasingFunctions.Linear) == 1f)
            {
                _clickAnimation.Reset();
                _clickAnimation.Show = false;
            }
        }
    }
}