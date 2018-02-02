using System.Collections.Generic;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Layout
{
    public class CenterLayout : ILayout
    {
        public void Refresh(Rectangle host, List<Control> childs)
        {
            foreach (var c in childs)
            {
                var origin = new Rectangle(c.Location, c.Size).GetAnchorPoint(c.Origine);
                var anchor = host.GetAnchorPoint(c.Anchor);
                
                c.ApplyLayout(new Rectangle(anchor - origin, c.Size));
                c.RefreshLayout();
            }
        }
    }
}