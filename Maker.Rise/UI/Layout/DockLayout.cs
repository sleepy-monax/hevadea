using System;
using System.Collections.Generic;
using Maker.Rise.Enums;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Layout
{
    [Obsolete]
    public class DockLayout : ILayout
    {
        public void Refresh(Rectangle host, List<Control> childs)
        {
            foreach (var c in childs)
            {
                if (!c.Visible) continue;
                
                Rectangle newBound = Rectangle.Empty;
                
                switch (c.Dock)
                {
                    case Dock.Top:
                        newBound = new Rectangle(host.X, host.Y,host.Width, c.Bound.Height);
                        host = new Rectangle(host.X, host.Y + c.Bound.Height, host.Width, host.Height - c.Bound.Height);
                        break;
                        
                    case Dock.Right:
                        newBound = new Rectangle(host.X + host.Width - c.Bound.Width, host.Y, c.Bound.Width, host.Height);
                        host = new Rectangle(host.X, host.Y, host.Width - c.Bound.Width, host.Height);
                        break;
                        
                    case Dock.Bottom:
                        newBound = new Rectangle(host.X, host.Y + host.Height - c.Bound.Height, host.Width, c.Bound.Height);
                        host = new Rectangle(host.X, host.Y, host.Width, host.Height - c.Bound.Height);
                        break;
                        
                    case Dock.Left:
                        newBound = new Rectangle(host.X, host.Y, c.Bound.Width, host.Height);
                        host = new Rectangle(host.X + c.Bound.Width, host.Y, host.Width - c.Bound.Width, host.Height);
                        break;
                        
                    case Dock.Fill:
                        newBound = new Rectangle(host.X, host.Y, host.Width, host.Height);
                        break;
                    case Dock.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                c.Bound = newBound;
            }
        }
    }
}