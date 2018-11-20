using System;
using Hevadea.Entities;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Items;

namespace Hevadea.Systems.InventorySystem
{
    public static class Extension
    {
        public static bool Equipe(this Entity entity, Item item)
        {
            return false;
        }

        public static bool Pickup(this Entity entity, Item item, int count)
        {
            return false;
        }

        public static bool DropHoldedItem(this Entity entity)
        {
            var holder = entity.GetComponent<ItemHolder>();

            if (holder != null && holder.HoldedItem != null)
            {


            }

            return false;
        }

        public static Item HoldedItem(this Entity entity)
        {
            return entity.GetComponent<ItemHolder>()?.HoldedItem ?? null;
        }

        public static bool HoldItem(this Entity entity, Item item)
        {
            var holder = entity.GetComponent<ItemHolder>();

            if (holder != null)
            {
                holder.HoldedItem = item;
                return true;
            }

            return false;
        }
    }
}
