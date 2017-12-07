using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.UI
{
    public class DialogBox : Control
    {
        private enum DialogBoxState { Hide, Show, Hidding, Showning }

        private Vector2 _textSize = Vector2.Zero;
        private string _text;
        private Animation animation = new Animation();

        private string Text
        {
            set
            {
                _text = Utils.Text.parseText(value, Font, Bound.Width);
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

        public DialogBox(UiManager ui) : base(ui)
        {
            Font = UI.Ress.font_arial;
            Text = "null";
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            var width = (int) (Bound.Width * 0.9f + Bound.Width * 0.1f * MathUtils.Interpolate(animation.TwoPhases));
            var height = (int) (Bound.Height * 0.9f + Bound.Height * 0.1f * MathUtils.Interpolate(animation.TwoPhases));
            var rect = new Rectangle(Bound.X + (Bound.Width - width) / 2,
                                     Bound.Y + (Bound.Height - height) / 2,
                                     width, height);

            spriteBatch.FillRectangle(rect, new Color(0, 0, 0, 200) * MathUtils.Interpolate(animation.TwoPhases));
            //UI.Game.GraphicsDevice.ScissorRectangle = rect;
            spriteBatch.DrawString(Font, _text, new Vector2(Bound.X + (Bound.Width / 2  - _textSize.X / 2),
                                                            Bound.Y + (Bound.Height / 2 - _textSize.Y / 2) + _textSize.Y * MathUtils.Interpolate(1f - animation.TwoPhases)),
                                                Color.White * animation.Linear);
            //spriteBatch.FillRectangle(new Rectangle(Bound.X, Bound.Y, (int)(Bound.Width), Bound.Height), Color.DimGray);
            //UI.Game.GraphicsDevice.ScissorRectangle = new Rectangle(0, 0, UI.Game.Graphics.GetWidth(), UI.Game.Graphics.GetHeight());
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            animation.Show = State == DialogBoxState.Showning;
            animation.Speed = 1f;
            animation.Update(gameTime);
        }
    }
}