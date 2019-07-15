using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Framework.Extension
{
    public enum TextAlignement { Center, Left, Right, Top, Bottom }

    public enum TextStyle { Regular, Bold, DropShadow, Rectangle }

    public static class SpritebatchExtension
    {
        public delegate void DrawTask(SpriteBatch spriteBatch, GameTime gameTime);

        public static void Begin(this SpriteBatch spriteBatch, SpriteBatchBeginState spriteBatchBeginState)
        {
            spriteBatch.Begin(spriteBatchBeginState.SortMode, spriteBatchBeginState.BlendState, spriteBatchBeginState.SamplerState, spriteBatchBeginState.DepthStencilState, spriteBatchBeginState.RasterizerState, spriteBatchBeginState.Effect, spriteBatchBeginState.TransformMatrix);
        }

        public static void BeginDrawEnd(this SpriteBatch sb, DrawTask task, GameTime gameTime = null, SpriteBatchBeginState state = null)
        {
            if (state != null)
            {
                sb.Begin(state);
            }
            else
            {
                sb.Begin();
            }

            task(sb, gameTime);
            sb.End();
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, float x, float y, float width, float height, Color color, float angle = 0.0f)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), null, color, angle, Vector2.Zero, new Vector2(width, height) * new Vector2(1f / texture.Width, 1f / texture.Height), SpriteEffects.None, 0);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 location, Vector2 size, Color color, float angle = 0.0f)
        {
            spriteBatch.Draw(texture, location, null, color, angle, Vector2.Zero, size * new Vector2(1f / texture.Width, 1f / texture.Height), SpriteEffects.None, 0);
        }

        public static void PutPixel(this SpriteBatch spriteBatch, float x, float y, Color color)
        {
            PutPixel(spriteBatch, new Vector2(x, y), color);
        }

        public static void PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), position, color);
        }

        public static void PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color, float size)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), position, null, color, 0, Vector2.Zero, new Vector2(size, size), SpriteEffects.None, 0);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            DrawLine(spriteBatch, point1, point2, color, 1.0f);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = Mathf.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color)
        {
            DrawLine(spriteBatch, point, length, angle, color, 1.0f);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), point, null, color, angle, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0);
        }

        public static void DrawPolygon(SpriteBatch spriteBatch, Vector2 position, List<Vector2> points, Color color, float thickness)
        {
            if (points.Count < 2)
                return;

            for (int i = 1; i < points.Count; i++)
            {
                DrawLine(spriteBatch, points[i - 1] + position, points[i] + position, color, thickness);
            }
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color)
        {
            DrawPolygon(spriteBatch, center, Geometry.Circle(radius, sides), color, 1.0f);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness)
        {
            DrawPolygon(spriteBatch, center, Geometry.Circle(radius, sides), color, thickness);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color)
        {
            DrawPolygon(spriteBatch, new Vector2(x, y), Geometry.Circle(radius, sides), color, 1.0f);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color, float thickness)
        {
            DrawPolygon(spriteBatch, new Vector2(x, y), Geometry.Circle(radius, sides), color, thickness);
        }

        public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color)
        {
            DrawArc(spriteBatch, center, radius, sides, startingAngle, radians, color, 1.0f);
        }

        public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float thickness)
        {
            var arc = Geometry.Arc(radius, sides, startingAngle, radians);
            DrawPolygon(spriteBatch, center, arc, color, thickness);
        }

        public static void DrawRectangle(this SpriteBatch sb, int x, int y, int w, int h, Color color, float thickness = 1.0f)
        {
            DrawRectangle(sb, new Rectangle(x, y, w, h), color, thickness);
        }

        public static void DrawRectangle(this SpriteBatch sb, Point begin, Point end, Color color, float thickness = 1.0f)
        {
            DrawLine(sb, new Vector2(begin.X, begin.Y), new Vector2(end.X, begin.Y), color, thickness); // Right
            DrawLine(sb, new Vector2(begin.X + thickness, begin.Y + thickness), new Vector2(begin.X + thickness, end.Y - thickness), color, thickness); // left
            DrawLine(sb, new Vector2(begin.X, end.Y - thickness), new Vector2(end.X, end.Y - thickness), color, thickness); // bottom
            DrawLine(sb, new Vector2(end.X, begin.Y + thickness), new Vector2(end.X, end.Y - thickness), color, thickness); // right
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness = 1.0f)
        {
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // Right
            DrawLine(spriteBatch, new Vector2(rect.X + thickness, rect.Y + thickness), new Vector2(rect.X + thickness, rect.Bottom - thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom - thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right, rect.Y + thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // right
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, RectangleF rect, Color color, float thickness = 1.0f)
        {
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // Right
            DrawLine(spriteBatch, new Vector2(rect.X + thickness, rect.Y + thickness), new Vector2(rect.X + thickness, rect.Bottom - thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom - thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right, rect.Y + thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // right
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1.0f);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float thickness)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y),
                color, thickness);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), rect, color);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle, Vector2 origine)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), rect, null, color, angle, origine, SpriteEffects.None, 0);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle = 0.0f)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), location, null, color, angle, Vector2.Zero, size, SpriteEffects.None, 0);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle, Vector2 origin)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), location, null, color, angle, origin, size, SpriteEffects.None, 0);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, 0.0f);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle, float ox, float oy)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle, new Vector2(ox, oy));
        }

        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle boundary, TextAlignement alignement, TextStyle style, Color color, float scale = 1f)
        {
            Vector2 textSize = font.MeasureString(text) * scale;
            Vector2 pos = boundary.Center.ToVector2();
            Vector2 origin = textSize / 2;

            if (alignement.HasFlag(TextAlignement.Left))
                origin.X += boundary.Width / 2f - textSize.X / 2;

            if (alignement.HasFlag(TextAlignement.Right))
                origin.X -= boundary.Width / 2f - textSize.X / 2;

            if (alignement.HasFlag(TextAlignement.Top))
                origin.Y += boundary.Height / 2f - textSize.Y / 2;

            if (alignement.HasFlag(TextAlignement.Bottom))
                origin.Y -= boundary.Height / 2f - textSize.Y / 2;

            switch (style)
            {
                case TextStyle.Bold:
                    spriteBatch.DrawString(font, text, new Vector2(pos.X + 1, pos.Y + 1), color, 0, origin, 1,
                        SpriteEffects.None, 0);
                    break;

                case TextStyle.DropShadow:
                    spriteBatch.DrawString(font, text, pos - origin + new Vector2(2 * Rise.Ui.ScaleFactor),
                        new Color(0, 0, 0, (int)(100f * (color.A / 255f))), 0, Vector2.Zero, scale,
                        SpriteEffects.None, 0);
                    break;

                case TextStyle.Rectangle:
                    spriteBatch.FillRectangle(
                        new Rectangle((pos - origin).ToPoint(),
                            new Point((int)textSize.X, (int)textSize.Y)), Color.Black * 0.5f);
                    break;
            }

            spriteBatch.DrawString(font, text, pos - origin, color, 0f, Vector2.Zero, new Vector2(scale, scale),
                SpriteEffects.None, 1f);
        }

        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle boundary, Color color, Anchor alignement = Anchor.TopLeft, float scale = 1f, Point? offset = null)
            => DrawString(spriteBatch, font, text, boundary, color, alignement, alignement, scale, offset);

        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle boundary, Color color, Anchor origin = Anchor.TopLeft, Anchor alignement = Anchor.TopLeft, float scale = 1f, Point? offset = null)
        {
            var size = font.MeasureString(text) * scale;

            var textBound = new Rectangle(Point.Zero, size.ToPoint());
            var position = boundary.Location + boundary.GetAnchorPoint(alignement) - textBound.GetAnchorPoint(origin) + (offset ?? Point.Zero);

            spriteBatch.DrawString(font, text, position.ToVector2(), color, 0f, Vector2.Zero, new Vector2(scale, scale),
                SpriteEffects.None, 1f);
        }

        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 pos, Color color, Anchor origin = Anchor.TopLeft, float scale = 1f, Vector2? offset = null)
        {
            var size = font.MeasureString(text) * scale;

            var textBound = new Rectangle(Point.Zero, size.ToPoint());

            spriteBatch.DrawString(font, text, pos - textBound.GetAnchorPoint(origin).ToVector2() + (offset ?? Vector2.Zero), color, 0f, Vector2.Zero, new Vector2(scale, scale), SpriteEffects.None, 0f);
        }
    }
}
