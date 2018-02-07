using Maker.Rise.Extension;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Widgets.Containers
{
    public class AnchoredContainer : Container
    {
        public override void Layout()
        {
            foreach (var c in Childrens)
            {
                var position = Host.Location + Host.GetAnchorPoint(c.Anchor) - c.Bound.GetAnchorPoint(c.Origine) + c.Offset;
                c.Bound = new Rectangle(position, c.Bound.Size);
            }
        }
    }
}