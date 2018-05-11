using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public static class DrawText
    {
        public enum Alignement { Center, Left, Right, Top, Bottom }
        public enum TextStyle { Regular, Bold, DropShadow, Rectangle }
        
        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle boundary, Alignement alignement, TextStyle style, Color color, float scale = 1f)
        {
            Vector2 textSize = font.MeasureString(text) * scale;
            Vector2 pos = boundary.Center.ToVector2();
            Vector2 origin = textSize / 2;

            if (alignement.HasFlag(Alignement.Left))
                origin.X += boundary.Width / 2f - textSize.X / 2;

            if (alignement.HasFlag(Alignement.Right))
                origin.X -= boundary.Width / 2f - textSize.X / 2;

            if (alignement.HasFlag(Alignement.Top))
                origin.Y += boundary.Height / 2f - textSize.Y / 2;

            if (alignement.HasFlag(Alignement.Bottom))
                origin.Y -= boundary.Height / 2f - textSize.Y / 2;

            switch (style)
            {
                case TextStyle.Bold:
                    spriteBatch.DrawString(font, text, new Vector2(pos.X + 1, pos.Y + 1), color, 0, origin, 1,
                        SpriteEffects.None, 0);
                    break;
                case TextStyle.DropShadow:
                    spriteBatch.DrawString(font, text, pos - origin + new Vector2(2 * Rise.Ui.ScaleFactor),
                        new Color(0, 0, 0, (int) (100f * ((float) color.A / 255f))), 0, Vector2.Zero, scale,
                        SpriteEffects.None, 0);
                    break;
                case TextStyle.Rectangle:
                    spriteBatch.FillRectangle(
                        new Rectangle((pos - origin - new Vector2(4)).ToPoint(),
                            new Point((int) textSize.X, (int) textSize.Y) + new Point(8)), Color.Black);
                    break;
            }

            spriteBatch.DrawString(font, text, pos - origin, color, 0f, Vector2.Zero, new Vector2(scale, scale),
                SpriteEffects.None, 1f);
        }
    }
}