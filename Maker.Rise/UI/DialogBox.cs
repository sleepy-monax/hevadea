using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI
{
    public class DialogBox : Control
    {
        private enum DialogBoxState
        {
            Hide,
            Show,
            Hidding,
            Showning
        }

        private Vector2 _textSize = Vector2.Zero;
        private string _text;
        private Animation animation = new Animation();

        private string Text
        {
            set
            {
                _text = Extension.Text.ParseText(value, Font, Bound.Width - 16);
                _textSize = Font.MeasureString(_text);
            }
        }

        private SpriteFont Font;
        private DialogBoxState State = DialogBoxState.Hide;

        public void Show(string text)
        {
            if (State == DialogBoxState.Hide || State == DialogBoxState.Hidding)
            {
                State = DialogBoxState.Showning;
            }

            Text = text;
        }

        public void Hide()
        {
            if (State == DialogBoxState.Show || State == DialogBoxState.Showning)
            {
                State = DialogBoxState.Hidding;
            }
        }

        public DialogBox()
        {
            Font = EngineRessources.FontArial;
            Text = "null";
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var width = (int) (Bound.Width * 0.9f + Bound.Width * 0.1f * animation.SinTwoPhases);
            var height = (int) (Bound.Height * 0.9f + Bound.Height * 0.1f * animation.SinTwoPhases);
            var rect = new Rectangle(Bound.X + (Bound.Width - width) / 2,
                Bound.Y + (Bound.Height - height) / 2,
                width, height);

            spriteBatch.FillRectangle(rect, new Color(0, 0, 0, 200) * animation.SinTwoPhases);
            spriteBatch.DrawString(Font, _text, new Vector2(Bound.X + (Bound.Width / 2 - _textSize.X / 2),
                    Bound.Y + (Bound.Height / 2 - _textSize.Y / 2) + _textSize.Y * (1f - animation.SinTwoPhases)),
                Color.White * animation.Linear);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            animation.Show = State == DialogBoxState.Showning;
            animation.Speed = 1f;
            animation.Update(gameTime);
        }
    }
}