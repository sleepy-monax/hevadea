using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Framework.UI
{
    public class LayoutDock : Layout
    {
        protected override void DoLayout()
        {
            var parentBound = new Rectangle(UnitHost.Location, UnitHost.Size);

            foreach (var c in Children)
            {
                if (c.Disabled) continue;

                var childBound = c.UnitBound;
                switch (c.Dock)
                {
                    case Dock.Top:
                        childBound = new Rectangle(parentBound.X, parentBound.Y, parentBound.Width, childBound.Height);
                        parentBound = new Rectangle(parentBound.X, parentBound.Y + childBound.Height, parentBound.Width,
                            parentBound.Height - childBound.Height);
                        break;

                    case Dock.Right:
                        childBound = new Rectangle(parentBound.X + parentBound.Width - childBound.Width, parentBound.Y,
                            childBound.Width, parentBound.Height);
                        parentBound = new Rectangle(parentBound.X, parentBound.Y, parentBound.Width - childBound.Width,
                            parentBound.Height);
                        break;

                    case Dock.Bottom:
                        childBound = new Rectangle(parentBound.X,
                            parentBound.Y + parentBound.Height - childBound.Height, parentBound.Width,
                            childBound.Height);
                        parentBound = new Rectangle(parentBound.X, parentBound.Y, parentBound.Width,
                            parentBound.Height - childBound.Height);
                        break;

                    case Dock.Left:
                        childBound = new Rectangle(parentBound.X, parentBound.Y, childBound.Width, parentBound.Height);
                        parentBound = new Rectangle(parentBound.X + childBound.Width, parentBound.Y,
                            parentBound.Width - childBound.Width, parentBound.Height);
                        break;

                    case Dock.Fill:
                        childBound = parentBound;
                        break;

                    case Dock.None:
                        var position = UnitHost.Location + UnitHost.GetAnchorPoint(c.Anchor) -
                                       c.UnitBound.GetAnchorPoint(c.Origine) + c.UnitOffset;
                        childBound = new Rectangle(position, c.UnitBound.Size);
                        break;
                }

                c.UnitBound = c.Spacing.Apply(childBound);
            }
        }
    }
}