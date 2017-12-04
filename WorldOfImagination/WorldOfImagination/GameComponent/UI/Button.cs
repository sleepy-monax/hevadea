using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.UI
{
    public class Button : Control
    {
        public string Text = "Button";
        public Texture2D Icon = null;
        
        public Button(UiManager ui) : base(ui)
        {
        }

        private float animation = 0f;
        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (MouseState == MouseState.Over)
            {
                animation = Math.Min(1f, animation + (float)(gameTime.ElapsedGameTime.Milliseconds) / 200f);
               
            }
            else
            {
                animation = Math.Max(0f, animation - (float)(gameTime.ElapsedGameTime.Milliseconds) / 100f);
            }

            var width = (int) (Bound.Width * MathUtils.Interpolate(animation));
            var height = (int) (Bound.Height * MathUtils.Interpolate(animation));
            //spriteBatch.DrawRectangle(new Rectangle(Bound.X + Bound.Width / 2 - width / 2, Bound.Y + Bound.Height / 2 - height / 2, width, height), new Color(255, 255, 255, (int)(100f * animation)));
            
            if (Icon != null)
            {
                //spriteBatch.Draw(Icon, new Vector2(Bound.X + (Bound.Width / 2 - Icon.Width / 2), Bound.Y + Bound.Height / 2 - (Icon.Height / 2) * animation), Color.White * animation);
                spriteBatch.Draw(Icon, new Rectangle(Bound.X + (Bound.Width / 2 - Icon.Width / 2), (int)(Bound.Y + Bound.Height / 2 - (Icon.Height / 2) * animation), Icon.Width, (int)(Icon.Height * (animation))), Color.White * animation);
                
                spriteBatch.DrawString(UI.Ress.bebas, Text, Bound, Alignement.Center, Style.DropShadow, Color.White * (1f - animation), 1f + animation);
            }
            else
            {
                spriteBatch.DrawString(UI.Ress.romulus, Text, Bound, Alignement.Center, Style.DropShadow, Color.White, 1f + animation / 3f);
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
        
        }
    }
}
