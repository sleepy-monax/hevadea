using System.Collections.Generic;
using Maker.Rise.Enums;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Layout
{
    public class FlowLayout : ILayout
    {        
        public void Refresh(Rectangle host, List<Control> childs)
        {
            int offset = 0;
            foreach (var c in childs)
            {
                c.ApplyLayout(new Rectangle(host.X, host.Y + offset, host.Width, c.Size.Y));
                offset += c.Size.Y;
            }
        }
    }
}