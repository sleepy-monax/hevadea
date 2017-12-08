using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.UI
{
    public class Button : Control
    {
        public string Text { get; set; } = "Button";
        public Texture2D Icon { get; set; } = null;

        public bool Dancing { get; set; } = true;
        private Animation animation = new Animation();
        private Animation clickAnimation = new Animation();
        private Animation downAnimation = new Animation();
        private Point OnMousClickPosition = Point.Zero;
        
        public Button(UiManager ui) : base(ui, true)
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
 
            var width = (int) (Bound.Width * animation.SinTwoPhases - 32f * downAnimation.SinTwoPhases);
            var height = (int) (Bound.Height * animation.SinTwoPhases - 8f * downAnimation.SinTwoPhases);
            
            spriteBatch.FillRectangle(new Rectangle(Bound.X + Bound.Width / 2 - width / 2, Bound.Y + Bound.Height / 2 - height / 2, width, height), new Color(0, 0, 0, (int)(200f * animation.TwoPhases)));
            spriteBatch.FillRectangle(new Rectangle((int)(OnMousClickPosition.X - width / 2f * clickAnimation.TwoPhases), Bound.Y + Bound.Height / 2 - height / 2, (int)(width*clickAnimation.TwoPhases), height), Color.Gold * ((1f - clickAnimation.TwoPhases) * 0.75f));
            
            if (Icon != null)
            {
                spriteBatch.Draw(Icon, new Vector2(Bound.X + (Bound.Width / 2 - Icon.Width / 2), Bound.Y + Bound.Height / 2 - (Icon.Height / 2) * animation.TwoPhases), Color.White * animation.TwoPhases);
                spriteBatch.DrawString(UI.Ress.font_bebas, Text, Bound, Alignement.Center, Style.DropShadow, Color.White * (1f - animation.Linear), 1f + animation.SinTwoPhases);
            }
            else
            {
                spriteBatch.DrawString(UI.Ress.font_bebas, Text, Bound, Alignement.Center, Style.DropShadow, Color.White, 1f + animation.TwoPhases / 3f);
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            animation.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;
            downAnimation.Show = MouseState == MouseState.Down;
            
            if (UI.Input.MouseLeftClick && Bound.Contains(UI.Input.MousePosition))
            {
                clickAnimation.Reset();
                clickAnimation.Show = true;
                clickAnimation.Speed = 1f;
                OnMousClickPosition = UI.Input.MousePosition;
            }

            if (clickAnimation.TwoPhases == 1f)
            {
                clickAnimation.Reset();
                clickAnimation.Show = false;
                if (Bound.Contains(UI.Input.MousePosition))
                {
                    RaiseOnMouseClick();
                }
            }
            
            animation.Update(gameTime);
            clickAnimation.Update(gameTime);
            downAnimation.Update(gameTime);
        }
    }
}
