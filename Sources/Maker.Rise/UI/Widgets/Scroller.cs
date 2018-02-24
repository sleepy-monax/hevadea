using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Rise.UI.Widgets
{
    public class Scroller : Widget
    {
        public Widget Content { get; set; } = null;
        private int _scrollOffset = 0;
        
        private int _contentHeight => Math.Max(Content?.Bound.Height ?? Host.Height, Host.Height);
        private float _thumbHeight => Host.Height * (Host.Height / (float)(_contentHeight));
        private float _thumbY => (-_scrollOffset / ((_contentHeight - Host.Height) / (Host.Height - _thumbHeight)));
        
        public override void RefreshLayout()
        {
            if (Content != null)
            {
                Content.Bound = new Rectangle(Host.X, Host.Y + _scrollOffset, Host.Width, Content.Bound.Height);
            }
            
            Content?.RefreshLayout();
        }

        public override void Update(GameTime gameTime)
        {
            Content?.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Content?.Draw(spriteBatch, gameTime);
            
        }
    }
}