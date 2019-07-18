using System;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Items;

namespace Hevadea.Systems.InventorySystem
{
    public static class Extension
    {
        public static Item HoldedItem(this Entity entity)
        {
            return entity.GetComponent<ComponentItemHolder>()?.HoldedItem ?? null;
        }

        public static bool HoldItem(this Entity entity, Item item)
        {
            var holder = entity.GetComponent<ComponentItemHolder>();

            if (holder != null)
            {
                holder.HoldedItem = item;
                return true;
            }

            return false;
        }
    }
}