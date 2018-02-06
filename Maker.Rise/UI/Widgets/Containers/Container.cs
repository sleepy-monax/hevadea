using System.Collections.Generic;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets.Containers
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
            if (Engine.Debug.Visible)
            {
                spriteBatch.DrawRectangle(Bound, Color.Red);
                spriteBatch.DrawRectangle(Bound, Color.Cyan);
            }
            
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
    }
}
