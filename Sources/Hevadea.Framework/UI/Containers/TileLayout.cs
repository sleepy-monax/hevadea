using Microsoft.Xna.Framework;

namespace Hevadea.Framework.UI.Containers
{
    public class TileLayout : Container
    {
        public FlowDirection Flow { get; set; } = FlowDirection.LeftToRight;
        public Padding Marging { get; set; } = new Padding(0);

        public override void Layout()
        {
            var count = 0;
            foreach (var c in Childrens)
            {
                if (c.IsEnable) count++;
            }

            if (count == 0) return;

            var itemWidth = UnitHost.Width / count;
            var itemHeight = UnitHost.Height / count;
            var index = 0;
            
            foreach (var c in Childrens)
            {
                switch (Flow)
                {
                    case FlowDirection.TopToBottom:
                        c.UnitBound = Marging.Apply(new Rectangle(UnitHost.X, UnitHost.Y + itemHeight * index, UnitHost.Width, itemHeight));
                        break;
                    case FlowDirection.BottomToTop:
                        c.UnitBound = Marging.Apply(new Rectangle(UnitHost.X, UnitHost.Y + UnitHost.Height - (itemHeight * (index + 1)), UnitHost.Width, itemHeight));
                        break;
                    case FlowDirection.LeftToRight:
                        c.UnitBound = Marging.Apply(new Rectangle(UnitHost.X + itemWidth * index, UnitHost.Y, itemWidth, UnitHost.Height));
                        break;
                    case FlowDirection.RightToLeft:
                        c.UnitBound = Marging.Apply(new Rectangle(UnitHost.X + UnitHost.Width - (itemWidth *  (1+index)), UnitHost.Y, itemWidth, UnitHost.Height));
                        break;
                }
                
                index++;
            }
        }
    }
}
