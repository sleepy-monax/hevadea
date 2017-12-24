using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.GameComponent.UI
{
    public class MainMenuButton : Control
    {
        public string Text { get; set; } = "Button";
        public string SubText { get; set; } = "do something";
        public Texture2D Icon { get; set; } = null;
        private SpriteFont Font;

        private Point OnMousClickPosition = Point.Zero;

        private Animation animation = new Animation();
        private Animation clickAnimation = new Animation();
        private Animation downAnimation = new Animation();


        public MainMenuButton(UiManager ui) : base(ui, false)
        {
            Font = UI.Ress.font_bebas;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var textSize = Font.MeasureString(Text);

            var invClickanim = (1f - clickAnimation.TwoPhases);


            var width = (int)(Bound.Width * animation.SinTwoPhases - 32f * downAnimation.SinTwoPhases);
            var height = (int)(Bound.Height * animation.SinTwoPhases - 8f * downAnimation.SinTwoPhases);

            var rectX = Bound.X + Bound.Width / 2 - width / 2;
            var rectY = Bound.Y + Bound.Height / 2 - height / 2;
            var rect = new Rectangle(rectX, rectY, width, height);

            var clickRectWidth = (int)(width * clickAnimation.SinTwoPhases);
            var clickRect = new Rectangle(rectX, rectY, clickRectWidth, height);

            spriteBatch.FillRectangle(rect, Color.Black *  animation.Linear);
            spriteBatch.FillRectangle(clickRect, Color.White * invClickanim);

            if (Icon != null)
            {
                var iconY = (Bound.Height / 2 - Icon.Height / 2);
                var iconX = iconY;
                spriteBatch.Draw(Icon, new Vector2(Bound.X + iconX, Bound.Y + iconY), Color.White);
                spriteBatch.DrawString(UI.Ress.font_bebas, Text, new Vector2(Bound.X + (iconX + Icon.Width + iconY) * animation.SinTwoPhases, Bound.Y + (Bound.Height / 2 - textSize.Y / 2) + 4), Color.White * animation.SinLinear);
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (OldMouseState == MouseState.None && MouseState == MouseState.Over)
            {
                UI.Game.Ress.menu_pick.Play();
            }

            animation.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;
            downAnimation.Show = MouseState == MouseState.Down;

            if (UI.Input.MouseLeftClick && Bound.Contains(UI.Input.MousePosition))
            {
                clickAnimation.Reset();
                clickAnimation.Show = true;
                clickAnimation.Speed = 1f;
                OnMousClickPosition = UI.Input.MousePosition;
                UI.Game.Ress.menu_select.Play();
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
