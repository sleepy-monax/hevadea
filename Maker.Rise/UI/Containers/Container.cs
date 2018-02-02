using System.Collections.Generic;
using Maker.Rise.Extension;
using Maker.Rise.UI.Layout;
using Maker.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Containers
{
    public class Container<TLayout> : IContainer where TLayout: ILayout, new()
    {
        public List<Control> Childs { get; set; } = new List<Control>();
        private TLayout _layout;
        private Rectangle _host = Rectangle.Empty;

        public Container()
        {
            _layout = new TLayout();
        }

        public void RefreshLayout(Rectangle host)
        {
            _host = host;
            _layout.Refresh(host, Childs);

            foreach (var c in Childs)
            {
                c.RefreshLayout();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in Childs.Clone())
            {
                c.Update(gameTime);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Engine.Debug.Visible) spriteBatch.FillRectangle(_host, Color.Black * 0.5f);
            foreach (var c in Childs)
            {
                c.Draw(spriteBatch, gameTime);
            } 
        }
    }
}