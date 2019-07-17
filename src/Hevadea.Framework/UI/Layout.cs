using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.UI
{
    public abstract class Layout : Widget
    {
        public List<Widget> Childrens { get; set; } = new List<Widget>();

        public Layout()
        { }

        public Layout(params Widget[] widgets)
        {
            Childrens.AddRange(widgets.Where(x => x != null));
        }

        public override void RefreshLayout()
        {
            DoLayout();

            foreach (var c in Childrens)
            {
                c.RefreshLayout();
            }
        }

        public abstract void DoLayout();

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

        public Layout AddChild<T>(T child) where T : Widget
        {
            Childrens.Add(child);

            return this;
        }

        public Layout AddChilds(params Widget[] childrens)
        {
            foreach (var c in childrens)
            {
                AddChild(c);
            }

            return this;
        }
    }
}
