using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI.Containers
{
    public class AnchoredContainer : Container
    {
        public override void Layout()
        {
            foreach (var c in Childrens)
            {
                var position = UnitHost.Location + UnitHost.GetAnchorPoint(c.Anchor) - c.UnitBound.GetAnchorPoint(c.Origine) + c.UnitOffset;
                c.UnitBound = new Rectangle(position, c.UnitBound.Size);
            }
        }
    }
}