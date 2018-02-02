using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Layout
{
    public class TileLayout : ILayout
    {
        public bool Vertical { get; set; } = true;
        public bool Horizontal
        {
            get => !Vertical;
            set => Vertical = !value;
        }

        public void Refresh(Rectangle host, List<Control> childs)
        {
            var count = childs.Count;
            var i = 0;
            var height = host.Height / count;
            var width = host.Width / count;
            
            foreach (var c in childs)
            {
                c.ApplyLayout(Vertical
                    ? new Rectangle(host.X, host.Y + height * i, host.Width, height)
                    : new Rectangle(host.X + width * i, host.Y, width, host.Height));

                i++;
            }

        }
    }
}