using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI
{
    public class MainMenuButton : Button
    {
        public string SubText { get; set; } = "do something";
        private SpriteFont Font;

        private Point OnMousClickPosition = Point.Zero;


        public MainMenuButton()
        {
            Font = EngineRessources.font_bebas;
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
                spriteBatch.DrawString(EngineRessources.font_bebas, Text, new Vector2(Bound.X + (iconX + Icon.Width + iconY) * animation.SinTwoPhases, Bound.Y + (Bound.Height / 2 - textSize.Y / 2) + 4), Color.White * animation.SinLinear);
            }
        }
    }
}
