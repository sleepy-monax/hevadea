using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI
{
    public class Border : Spacing
    {
        public Color Color { get; set; } = Color.Black;

        public Border()
        {}

        public Border(int all) : base(all)
        {}

        public Border(int horizontal, int vertical) : base(horizontal, vertical)
        {}

        public Border(int top, int bottom, int left, int right) : base(top, bottom, left, right)
        {}
    }
}