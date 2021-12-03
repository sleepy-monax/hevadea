using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Systems.InventorySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuProcessor : EntityUpdateSystem
    {
        public CircleMenuProcessor()
        {
            Filter.AllOf(typeof(CircleMenu), typeof(ComponentInventory));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var menu = entity.GetComponent<CircleMenu>();
            var inventory = entity.GetComponent<ComponentInventory>();

            if (Rise.Input.KeyTyped(Keys.U))
            {
                menu.SelectedItem = (menu.SelectedItem - 1) % inventory.Content.GetStackCount();
                if (menu.SelectedItem < 0) menu.SelectedItem = inventory.Content.GetStackCount() - 1;

                menu.Shown();
            }
            else if (Rise.Input.KeyTyped(Keys.I))
            {
                menu.SelectedItem = (menu.SelectedItem + 1) % inventory.Content.GetStackCount();
                menu.Shown();
            }

            entity.HoldItem(inventory.Content.GetStack(menu.SelectedItem));

            menu.UpdateAnimation(gameTime);
        }
    }
}