using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI
{
    public class LayoutTile : Layout
    {
        public LayoutTileDirection Flow { get; set; } = LayoutTileDirection.LeftToRight;
        public Margins Marging { get; set; } = new Margins(0);

        public override void DoLayout()
        {
            var count = 0;
            foreach (var c in Childrens)
            {
                if (c.Enabled) count++;
            }

            if (count == 0) return;

            var itemWidth = UnitHost.Width / count;
            var itemHeight = UnitHost.Height / count;
            var index = 0;

            foreach (var c in Childrens)
            {
                switch (Flow)
                {
                    case LayoutTileDirection.TopToBottom:
                        c.UnitBound = c.Margin.Apply(new Rectangle(UnitHost.X, UnitHost.Y + itemHeight * index, UnitHost.Width, itemHeight));
                        break;

                    case LayoutTileDirection.BottomToTop:
                        c.UnitBound = c.Margin.Apply(new Rectangle(UnitHost.X, UnitHost.Y + UnitHost.Height - (itemHeight * (index + 1)), UnitHost.Width, itemHeight));
                        break;

                    case LayoutTileDirection.LeftToRight:
                        c.UnitBound = c.Margin.Apply(new Rectangle(UnitHost.X + itemWidth * index, UnitHost.Y, itemWidth, UnitHost.Height));
                        break;

                    case LayoutTileDirection.RightToLeft:
                        c.UnitBound = c.Margin.Apply(new Rectangle(UnitHost.X + UnitHost.Width - (itemWidth * (1 + index)), UnitHost.Y, itemWidth, UnitHost.Height));
                        break;
                }

                index++;
            }
        }
    }
}