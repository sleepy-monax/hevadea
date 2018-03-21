using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Framework.UI.Containers
{
    public enum FlowDirection
    {
        TopToBottom, BottomToTop, LeftToRight, RightToLeft
    }
    
    public abstract class Container : Widget
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

        public abstract void Layout();

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
    }
}
