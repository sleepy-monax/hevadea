using Maker.Rise.Enum;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI
{
    public class Button : Control
    {
        public string Text { get; set; } = "Button";
        public Texture2D Icon { get; set; } = null;

        public bool Dancing { get; set; } = true;
        public Animation animation = new Animation();
        public Animation clickAnimation = new Animation();
        private Point OnMousClickPosition = Point.Zero;

        public Button()
        {
            animation.Speed = 1f;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var invClickanim = (1f - clickAnimation.TwoPhases);

            var lineWidth = 2.5f + 5f * (1f - animation.Linear);
            
            var width = (int) (Bound.Width + 8f * animation.SinTwoPhases - 16f);
            var height = (int) (Bound.Height + 8f * animation.SinTwoPhases - 16f);

            var rectX = Bound.X + Bound.Width / 2 - width / 2;
            var rectY = Bound.Y + Bound.Height / 2 - height / 2;
            var rect = new Rectangle(rectX, rectY, width, height);

            var clickRectWidth = (int) (width * clickAnimation.SinTwoPhases);
            var clickRect = new Rectangle(rectX + width / 2 - clickRectWidth / 2, rectY, clickRectWidth, height);

            spriteBatch.FillRectangle(new Rectangle(rectX + 4, rectY + 4, width, height), Color.Black * 0.1f);
            
            spriteBatch.FillRectangle(rect, new Color(0x54, 0x52, 0x9b) * (1f - animation.Linear));
            spriteBatch.FillRectangle(rect, new Color(0x34, 0x33, 0x60) * animation.Linear);
            
            spriteBatch.FillRectangle(clickRect, Color.White * invClickanim);
            spriteBatch.DrawLine(rectX, rectY, rectX + width, rectY, Color.White * 0.25f, lineWidth);
            spriteBatch.DrawLine(rectX + lineWidth, rectY, rectX + lineWidth, rectY + height, Color.White * 0.25f, lineWidth);
            
            spriteBatch.DrawLine(rectX, rectY + height - lineWidth, rectX + width, rectY + height - lineWidth, Color.Black * 0.25f, lineWidth);
            spriteBatch.DrawLine(rectX + width, rectY, rectX + width, rectY + height, Color.Black * 0.25f, lineWidth);

            if (Icon != null)
            {
                var iconY = (Bound.Height / 2 - Icon.Height / 2);

                spriteBatch.Draw(Icon, new Vector2(Bound.X + iconY, Bound.Y + iconY), Color.White);
                spriteBatch.DrawString(EngineRessources.FontBebas, Text, Bound, Alignement.Center, TextStyle.DropShadow,
                    Color.White);
            }
            else
            {
                spriteBatch.DrawString(EngineRessources.FontBebas, Text, Bound, Alignement.Center, TextStyle.DropShadow,
                    Color.White, 1f + animation.TwoPhases / 3f);
            }
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
                clickAnimation.Speed = 1f;
                OnMousClickPosition = Engine.Input.MousePosition;
                Engine.Audio.PlaySoundEffect(EngineRessources.MenuSelect);
            }

            if (clickAnimation.TwoPhases == 1f)
            {
                clickAnimation.Reset();
                clickAnimation.Show = false;
            }

            animation.Update(gameTime);
            clickAnimation.Update(gameTime);
        }
    }
}