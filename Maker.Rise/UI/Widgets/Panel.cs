using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class Panel : Widget
    {
        public Widget Content { get; set; } = null;

        public override void RefreshLayout()
        {
            if (Content != null)
            {
                Content.Bound = Host;
                Content.RefreshLayout();
            }
        }

        public override void Update(GameTime gameTime)
        {
            Content?.UpdateInternal(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Content?.DrawIternal(spriteBatch, gameTime);
        }
    }
}