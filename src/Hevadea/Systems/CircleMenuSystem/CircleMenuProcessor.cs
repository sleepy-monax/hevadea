using System;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Registry;
using Hevadea.Systems.InventorySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuProcessor : EntityUpdateSystem
    {
        public CircleMenuProcessor()
        {
            Filter.AnyOf(typeof(CircleMenu), typeof(Inventory));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var menu = entity.GetComponent<CircleMenu>();
            var inventory = entity.GetComponent<Inventory>();

            if (Rise.Input.KeyTyped(Keys.U))
            {
                menu.SelectedItem = MathHelper.Clamp(menu.SelectedItem - 1, 0, inventory.Content.GetStackCount() - 1);
                menu.Shown();
            }
            else if (Rise.Input.KeyTyped(Keys.I))
            {
                menu.SelectedItem = MathHelper.Clamp(menu.SelectedItem + 1, 0, inventory.Content.GetStackCount() - 1);
                menu.Shown();
            }

            entity.HoldItem(inventory.Content.GetStack(menu.SelectedItem));

            menu.UpdateAnimation(gameTime);
        }
    }
}
