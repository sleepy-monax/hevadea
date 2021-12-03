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
        public List<Widget> Children { get; set; } = new List<Widget>();

        public override void RefreshLayout()
        {
            DoLayout();

            foreach (var c in Children) c.RefreshLayout();
        }

        protected abstract void DoLayout();

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in Children) c.DrawIternal(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var c in Children) c.UpdateInternal(gameTime);
        }

        public Layout AddChild<T>(T child) where T : Widget
        {
            Children.Add(child);

            return this;
        }

        public Layout AddChildren(params Widget[] children)
        {
            foreach (var c in children) AddChild(c);

            return this;
        }
    }
}