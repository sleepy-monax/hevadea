using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI.Containers
{
    public class TileContainer : Container
    {
        public FlowDirection Flow { get; set; } = FlowDirection.LeftToRight;
        public Padding Marging { get; set; } = new Padding(0);

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
                        c.Bound = Marging.Apply(new Rectangle(Host.X, Host.Y + itemHeight * index, Host.Width, itemHeight));
                        break;
                    case FlowDirection.BottomToTop:
                        c.Bound = Marging.Apply(new Rectangle(Host.X, Host.Y + Host.Height - (itemHeight * index), Host.Width, itemHeight));
                        break;
                    case FlowDirection.LeftToRight:
                        c.Bound = Marging.Apply(new Rectangle(Host.X + itemWidth * index, Host.Y, itemWidth, Host.Height));
                        break;
                    case FlowDirection.RightToLeft:
                        c.Bound = Marging.Apply(new Rectangle(Host.X + Host.Width - (itemWidth * index), Host.Y, itemWidth, Host.Height));
                        break;
                }
                
                index++;
            }
        }
    }
}
