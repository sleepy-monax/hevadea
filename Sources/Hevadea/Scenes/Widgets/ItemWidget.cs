using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Scenes.Widgets
{
    public class ItemWidget : Widget
    {
        public Item Item { get; set; } = null;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Item != null)
            {
                var size = Math.Min(Host.Width, Host.Height);
                var minSize = size - 4;

                spriteBatch.FillRectangle(Bound, Color.Gold * 0.5f);
                spriteBatch.DrawRectangle(Bound, Color.Gold * 0.5f);

                Item.GetSprite().Draw(spriteBatch,
                    new Rectangle(Host.Location + new Point(size / 2) - new Point(minSize / 2),
                        new Point(minSize, minSize)), Color.White);
            }
        }
    }
}