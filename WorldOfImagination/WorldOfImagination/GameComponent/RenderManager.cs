using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent
{
    public class RenderManager
    {
        SpriteBatch sb;


        public void Begin()
        {
            sb.Begin();
        }

        public void SetColor(Color color) { }
        public void Translate() { }
        public void Scale() { }
        public void Rotate() { }

        public void Rectangle(bool fill, Rectangle rectangle, float weight = 1f) { }
        public void Line(Point start, Point end, float weight = 1f) { }
        public void Point(Point point, float weight = 1f) { }
        public void Text(SpriteFont font, string text, Point position, float size = 1f) { }

        public void End()
        {
            sb.End();
        }
    }
}
