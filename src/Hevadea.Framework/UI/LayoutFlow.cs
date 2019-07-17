using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI
{
    public class LayoutFlow : Layout
    {
        public int Marging { get; set; } = 0;
        public LayoutFlowDirection Flow { get; set; } = LayoutFlowDirection.LeftToRight;

        public override void DoLayout()
        {
            if (Childrens.Count == 0) return;
            int offset = 0;

            foreach (var c in Childrens)
            {
                if (c.Disabled) continue;

                switch (Flow)
                {
                    case LayoutFlowDirection.TopToBottom:
                        c.UnitBound = new Rectangle(UnitHost.X, UnitHost.Y + offset, UnitHost.Width, c.UnitBound.Height);
                        offset += c.UnitBound.Height;
                        break;

                    case LayoutFlowDirection.BottomToTop:
                        c.UnitBound = new Rectangle(UnitHost.X, UnitHost.Y + UnitHost.Height - offset, UnitHost.Width, c.UnitBound.Height);
                        offset += c.UnitBound.Height;
                        break;

                    case LayoutFlowDirection.LeftToRight:
                        c.UnitBound = new Rectangle(UnitHost.X + offset, UnitHost.Y, c.UnitBound.Width, UnitHost.Height);
                        offset += c.UnitBound.Width;
                        break;

                    case LayoutFlowDirection.RightToLeft:
                        c.UnitBound = new Rectangle(UnitHost.X + UnitHost.Width - offset, UnitHost.Y, c.UnitBound.Width, UnitHost.Height);
                        offset += c.UnitBound.Width;
                        break;
                }

                offset += Marging;
            }
        }
    }
}