using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hevadea.Framework.Utils;

namespace Hevadea.Framework.UI.Containers
{
    public enum FlowDirection
    {
        TopToBottom, BottomToTop, LeftToRight, RightToLeft
    }
    
    public class Container : Widget
    {
        public List<Widget> Childrens { get; set; } = new List<Widget>();

        public override void RefreshLayout()
        {
            Layout();

            foreach (var c in Childrens)
            {
                c.RefreshLayout();
            }
        }

        public virtual void Layout()
        {
            var h = new Rectangle(UnitHost.Location, UnitHost.Size);

            foreach (var c in Childrens)
            {
                if (c.IsDisable) continue;

                var b = c.UnitBound;
                switch (c.Dock)
                {
                    case Dock.Top:
                        b = new Rectangle(h.X, h.Y, h.Width, b.Height);
                        h = new Rectangle(h.X, h.Y + b.Height, h.Width, h.Height - b.Height);
                        break;
                    case Dock.Right:
                        b = new Rectangle(h.X + h.Width - b.Width, h.Y, b.Width, h.Height);
                        h = new Rectangle(h.X, h.Y , h.Width - b.Width, h.Height);
                        break;
                    case Dock.Bottom:
                        b = new Rectangle(h.X, h.Y + h.Height - b.Height, h.Width, b.Height);
                        h = new Rectangle(h.X, h.Y, h.Width, h.Height - b.Height);
                        break;
                    case Dock.Left:
                        b = new Rectangle(h.X, h.Y, b.Width, h.Height);
                        h = new Rectangle(h.X + b.Width, h.Y, h.Width - b.Width, h.Height);
                        break;
                    case Dock.Fill:
                        b = h;
                        break;
                    case Dock.None:
                        var position = UnitHost.Location + UnitHost.GetAnchorPoint(c.Anchor) - c.UnitBound.GetAnchorPoint(c.Origine) + c.UnitOffset;
                        c.UnitBound = new Rectangle(position, c.UnitBound.Size);
                        break;
                }
                
                c.UnitBound = b;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {            
            foreach (var c in Childrens)
            {
                c.DrawIternal(spriteBatch, gameTime);
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var c in Childrens)
            {
                c.UpdateInternal(gameTime);
            }
        }

        public T AddChild<T>(T child) where  T : Widget
        {
            Childrens.Add(child);
            return child;
        }

        public void AddChilds(params Widget[] childrens)
        {
            foreach (var c in childrens)
            {
                AddChild(c);
            }
        }
    }
}
