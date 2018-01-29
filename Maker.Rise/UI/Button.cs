using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Rise.UI
{
    public class Button : Control
    {
        public string Text { get; set; } = "Button";
        public Texture2D Icon { get; set; } = null;
        public bool EnableBlur { get; set; } = false;

        public bool Dancing { get; set; } = true;
        public FadingAnimation animation = new FadingAnimation();
        public FadingAnimation clickAnimation = new FadingAnimation();
        private Point OnMousClickPosition = Point.Zero;

        public Button()
        {
            animation.Speed = 1f;
            clickAnimation.Speed = 0.5f;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            animation.Update(gameTime);
            clickAnimation.Update(gameTime);

            var invClickanim = (1f - clickAnimation.GetValue(EasingFunctions.Linear));
            
            var width = (int) (Bound.Width + 8f * animation.GetValue(EasingFunctions.SineEaseIn) - 16f);
            var height = (int) (Bound.Height + 8f * animation.GetValue(EasingFunctions.SineEaseIn) - 16f);

            var rectX = Bound.X + Bound.Width / 2 - width / 2;
            var rectY = Bound.Y + Bound.Height / 2 - height / 2;
            var rect = new Rectangle(rectX, rectY, width, height);

            var clickRectWidth =  (int)(width * clickAnimation.GetValue(EasingFunctions.QuadraticEaseInOut));
            var clickRectHeight = (int)(height* clickAnimation.GetValue(EasingFunctions.QuadraticEaseInOut));

            var clickRect = new Rectangle(rectX + (int)Math.Min(Math.Max(OnMousClickPosition.X - clickRectWidth / 2, 0f), width - clickRectWidth),
                                          rectY + (int)Math.Min(Math.Max(OnMousClickPosition.Y - clickRectHeight / 2, 0f), height - clickRectHeight),
                
                                          clickRectWidth, clickRectHeight);

            //spriteBatch.FillRectangle(new Rectangle(rectX + 4, rectY + 4, width, height), Color.Black * 0.25f * animation.GetValue(EasingFunctions.Linear));

            if (EnableBlur) spriteBatch.Draw(Engine.Scene.BlurRT, rect, rect, Color.White);
            spriteBatch.FillRectangle(rect, Color.White * (1f - animation.GetValue(EasingFunctions.Linear)) * 0.05f);
            spriteBatch.FillRectangle(rect, Color.Black * animation.GetValue(EasingFunctions.Linear) * 0.75f);
            spriteBatch.DrawRectangle(rect, Color.White * 0.05f);

            
            spriteBatch.FillRectangle(clickRect, Color.White * invClickanim);

            if (Icon != null)
            {
                var iconY = (Bound.Height / 2 - Icon.Height / 2);

                spriteBatch.Draw(Icon, new Vector2(Bound.X + iconY, Bound.Y + iconY), Color.White);
            }

                
                spriteBatch.DrawString(EngineRessources.FontBebas, Text, new Rectangle(Bound.Location + new Point(0, 4), Bound.Size) , Alignement.Center, TextStyle.Regular,
                    Color.White);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (OldMouseState == MouseState.None && MouseState == MouseState.Over)
            {
                Engine.Audio.PlaySoundEffect(EngineRessources.MenuPick);
            }

            animation.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;

            if (Engine.Input.MouseLeftClick && Bound.Contains(Engine.Input.MousePosition))
            {
                clickAnimation.Reset();
                clickAnimation.Show = true;
                OnMousClickPosition = new Point(Engine.Input.MousePosition.X - Bound.X, Engine.Input.MousePosition.Y - Bound.Y);
                Engine.Audio.PlaySoundEffect(EngineRessources.MenuSelect);
            }

            if (clickAnimation.GetValue(EasingFunctions.ElasticEaseIn) == 1f)
            {
                clickAnimation.Reset();
                clickAnimation.Show = false;
            }
        }
    }
}