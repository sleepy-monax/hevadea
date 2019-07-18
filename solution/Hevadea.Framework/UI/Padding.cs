using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI
{
    public class Padding : Spacing
    {
        public Padding()
        {
        }

        public Padding(int all) : base(all)
        {
        }

        public Padding(int horizontal, int vertical) : base(horizontal, vertical)
        {
        }

        public Padding(int top, int bottom, int left, int right) : base(top, bottom, left, right)
        {
        }
    }
}