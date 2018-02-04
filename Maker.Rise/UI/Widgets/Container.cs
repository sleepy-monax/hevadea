using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public abstract class Container : Widget
    {
        public List<Widget> Childrens { get; set; } = new List<Widget>();

        public void RefreshLayout()
        {
            Layout();

            foreach (var c in Childrens)
            {
                if (c is Container cont) { cont.RefreshLayout(); }
            }
        }

        public abstract void Layout();

        public sealed override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in Childrens)
            {
                c.Draw(spriteBatch, gameTime);
            }
        }
        public sealed override void Update(GameTime gameTime)
        {
            foreach (var c in Childrens)
            {
                c.UpdateInternal(gameTime);
            }
        }
    }
}
