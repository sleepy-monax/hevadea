using Microsoft.Xna.Framework;
using WorldOfImagination.GameComponent.UI;

namespace WorldOfImagination.Utils
{
    public static class RectangleUtils
    {
        public static Point GetAnchor(this Rectangle rect, Anchor anchor)
        {
            switch (anchor) {
                case UpLeft:
                    return new Point(0, 0);
                case Up:
                    return new Point(rect.Width / 2, 0);
                case UpRight:
                    return new Point(rect.Width, 0)
                case Left:
                    return new Point(0, rect.Height / 2);
                case center:
                    return new Point(rect.Width / 2, rect.Height / 2);
                case Right:
                    return new Point(rect.Width, rect.Height / 2);
                case DownLeft:
                    return new Point(0, rect.Height);
                case Down:
                    return new Point(rect.Width / 2, rect.Height);
                case DownRight:
                    return new Point(rect.Width, rect.Height);
            }
        }
    }
}
