using Maker.Hevadea.Game.Items;
using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.UI
{
    public class ItemFrameControl : Widget
    {

        public Item Item { get; set; } = null;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Item != null)
            {
                var size = Math.Min(Host.Width, Host.Height);
                int minSize = (int)(size - 4);

                spriteBatch.Draw(Engine.Scene.BluredScene, Bound, Bound, Color.White);
                spriteBatch.FillRectangle(Bound, Color.Gold * 0.5f);
                spriteBatch.DrawRectangle(Bound, Color.Gold * 0.5f);

                Item.GetSprite().Draw(spriteBatch, new Rectangle(Host.Location + new Point(size / 2) - new Point(minSize / 2),new Point( minSize, minSize)), Color.White);
            }
        }
    }
}
