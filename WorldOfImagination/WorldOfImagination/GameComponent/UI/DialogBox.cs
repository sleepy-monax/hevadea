using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.UI
{
    public class DialogBox : Control
    {
        private enum DialogBoxState { Hide, Show, Hidding, Showning }

        private float      animation = 0f;
        private DialogBoxState State = DialogBoxState.Hide;
        
        public void Show()
        {
            if (State == DialogBoxState.Hide || State == DialogBoxState.Hidding)
            {
                State = DialogBoxState.Showning;
            }
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
            
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           spriteBatch.FillRectangle(new Rectangle(Bound.X, Bound.Y, (int)(Bound.Width * MathUtils.Interpolate(animation)), Bound.Height), Color.DimGray);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            switch (State)
            {
                case DialogBoxState.Hide    : break;
                case DialogBoxState.Show    : break;
                case DialogBoxState.Hidding : animation = Math.Max(0f, animation - gameTime.ElapsedGameTime.Milliseconds / 1000f); break;
                case DialogBoxState.Showning: animation = Math.Min(1f, animation + gameTime.ElapsedGameTime.Milliseconds / 1000f); break;
            }
        }
    }
}