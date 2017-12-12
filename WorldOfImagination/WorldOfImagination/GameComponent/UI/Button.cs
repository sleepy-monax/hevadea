using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        
        public Button(UiManager ui) : base(ui, false)
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var invClickanim = (1f - clickAnimation.TwoPhases);


            var width = (int) (Bound.Width * animation.SinTwoPhases - 32f * downAnimation.SinTwoPhases);
            var height = (int) (Bound.Height * animation.SinTwoPhases - 8f * downAnimation.SinTwoPhases);

            var rectX = Bound.X + Bound.Width / 2 - width / 2;
            var rectY = Bound.Y + Bound.Height / 2 - height / 2;
            var rect = new Rectangle(rectX, rectY, width, height);

            var clickRectWidth = (int)(width * clickAnimation.SinTwoPhases);
            var clickRect = new Rectangle(rectX + width / 2 - clickRectWidth / 2, rectY, clickRectWidth, height);

            spriteBatch.FillRectangle(rect, Color.Black * animation.TwoPhases);
            spriteBatch.FillRectangle(clickRect, Color.White * invClickanim);

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
            }
            
            animation.Update(gameTime);
            clickAnimation.Update(gameTime);
            downAnimation.Update(gameTime);
        }
    }
}
