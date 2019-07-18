using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI
{
    public class Margin : Spacing
    {
        public Color Color { get; set; } = Color.Transparent;

        public Margin()
        {
        }

        public Margin(int all) : base(all)
        {
        }

        public Margin(int horizontal, int vertical) : base(horizontal, vertical)
        {
        }

        public Margin(int top, int bottom, int left, int right) : base(top, bottom, left, right)
        {
        }
    }
}