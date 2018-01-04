
using Maker.Hevadea.Game.Items;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.UI
{
    public class InventoryUI : Control
    {
        Inventory Inventory;
        public InventoryUI(Inventory i)
        {
            Inventory = i;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var item in Inventory.Items)
            {

            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
