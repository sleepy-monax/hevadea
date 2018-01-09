using Maker.Hevadea.Game.Items;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class InventoryComponent : EntityComponent, IDrawableComponent, IUpdatableComponent
    {
        public Inventory Inventory { get; private set; }
        public bool AlowPickUp { get; set; } = true;
        private Item lastAdded;
        private readonly Animation anim = new Animation();

        public InventoryComponent(int slotCount)
        {
            Inventory = new Inventory{Capacity = slotCount};
        }
        
        public bool Pickup(Item item)
        {
            if (AlowPickUp && Inventory.AddItem(item))
            {
                anim.Reset();
                anim.Show = true;
                anim.Speed = 0.50f;

                lastAdded = item;
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            anim.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var v = 1f - anim.SinTwoPhases;
            lastAdded?.Sprite.Draw(spriteBatch, new Vector2(Owner.X + Owner.Width / 2f - 8 * v, Owner.Y+ Owner.Height / 2 - 24 * v), v, Color.White);
        }
    }
}