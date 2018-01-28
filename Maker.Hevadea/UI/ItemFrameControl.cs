using Maker.Hevadea.Game.Items;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.UI
{
    public class ItemFrameControl : Control
    {

        public Item Item { get; set; } = null;

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Item != null)
            {
                var size = Math.Min(Host.Width, Host.Height);
                int minSize = (int)(size - 4);

                spriteBatch.FillRectangle(new Rectangle(Host.X + 4, Host.Y + 4, size, size), Color.Black * 0.25f);
                spriteBatch.FillRectangle(new Rectangle(Host.X, Host.Y, size, size), Color.Gray);

                Item.GetSprite().Draw(spriteBatch, new Rectangle(Host.Location + new Point(size / 2) - new Point(minSize / 2),new Point( minSize, minSize)), Color.White);
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
