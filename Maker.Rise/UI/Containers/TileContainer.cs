using Maker.Rise.Enums;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Widgets.Containers
{
    public class TileContainer : Container
    {
        public FlowDirection Flow { get; set; } = FlowDirection.LeftToRight;
        public override void Layout()
        {
            var itemWidth = Host.Width / Childrens.Count;
            var itemHeight = Host.Height / Childrens.Count;
            var index = 0;
            
            foreach (var c in Childrens)
            {
                switch (Flow)
                {
                    case FlowDirection.TopToBottom:
                        c.Bound = new Rectangle(Host.X, Host.Y + itemHeight * index, Host.Width, itemHeight);
                        break;
                    case FlowDirection.BottomToTop:
                        c.Bound = new Rectangle(Host.X, Host.Y + Host.Height - (itemHeight * index), Host.Width, itemHeight);
                        break;
                    case FlowDirection.LeftToRight:
                        c.Bound = new Rectangle(Host.X + itemWidth * index, Host.Y, itemWidth, Host.Height);
                        break;
                    case FlowDirection.RightToLeft:
                        c.Bound = new Rectangle(Host.X + Host.Width - (itemWidth * index), Host.Y, itemWidth, Host.Height);
                        break;
                }
                
                index++;
            }
        }
    }
}
