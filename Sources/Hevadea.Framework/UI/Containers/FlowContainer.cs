using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI.Containers
{
    public class FlowContainer : Container
    {
        public FlowDirection Flow { get; set; } = FlowDirection.LeftToRight;

        public override void Layout()
        {
            if (Childrens.Count == 0) return;
            int offset = 0;

            foreach (var c in Childrens)
            {
                switch (Flow)
                {
                    case FlowDirection.TopToBottom:
                        c.UnitBound = new Rectangle(UnitHost.X, UnitHost.Y + offset, UnitHost.Width, c.UnitBound.Height);
                        offset += c.UnitBound.Height;
                        break;
                    case FlowDirection.BottomToTop:
                        c.UnitBound = new Rectangle(UnitHost.X, UnitHost.Y + UnitHost.Height - offset, UnitHost.Width, c.UnitBound.Height);
                        offset += c.UnitBound.Height;
                        break;
                    case FlowDirection.LeftToRight:
                        c.UnitBound = new Rectangle(UnitHost.X + offset, UnitHost.Y, c.UnitBound.Width, UnitHost.Height);
                        offset += c.UnitBound.Width;
                        break;
                    case FlowDirection.RightToLeft:
                        c.UnitBound = new Rectangle(UnitHost.X + UnitHost.Width - offset, UnitHost.Y, c.UnitBound.Width, UnitHost.Height);
                        offset += c.UnitBound.Width;
                        break;
                }
            }
        }
    }
}
