using Maker.Hevadea.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class InventoryComponent : EntityComponent
    {
        public readonly Inventory Inventory;
        public bool CanPickUp { get; set; } = true;

        private Item LastAdded = null;
        private int dt = 0;

        public InventoryComponent(int slotCount)
        {
            Inventory = new Inventory{capacity = slotCount};
        }
        
        public bool Pickup(Item item)
        {
            if (CanPickUp && Inventory.AddItem(item))
            {
                LastAdded = item;
                return true;
            }
            return false ;
        }

        public override void Update(GameTime gameTime)
        {
            if (dt > 64)
            {
                LastAdded = null;
                dt = 0;
            }
            
            if (LastAdded != null)
            {
                dt++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            LastAdded?.Sprite.Draw(spriteBatch, new Vector2(Owner.X + Owner.Width / 2f, Owner.Y+ Owner.Height / 2 - dt), 0.5f, Color.White);
        }
    }
}