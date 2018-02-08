using System;
using System.Collections.Generic;
using Maker.Hevadea.Game.Registry;

namespace Maker.Hevadea.Game.Items
{
    public class ItemStorage
    {
        public ItemStorage(int capacity = 256)
        {
            Items = new List<int>(capacity);
            Capacity = capacity;
        }

        public List<int> Items { get; set; }
        public int Capacity { get; }


        public int Add(Item item, int quantity)
        {
            var count = quantity;
            for (var i = 0; i < quantity; i++)
                if (Add(item))
                    count--;

            return count;
        }

        public bool Add(Item item)
        {
            if (HasFreeSlot())
            {
                Items.Add(item.Id);
                return true;
            }

            return false;
        }

        public bool HasFreeSlot()
        {
            return Items.Count < Capacity;
        }

        public int Count(Item item)
        {
            if (item == null) return 0;

            var count = 0;
            for (var i = 0; i < Items.Count; i++)
                if (Items[i] == item.Id)
                    count++;
            return count;
        }

        public void Remove(Item item, int quantity)
        {
            for (var i = 0; i < Items.Count; i++)
                if (Items[i] == item.Id && quantity > 0)
                {
                    Items.Remove(item.Id);
                    quantity--;
                    i--;
                }
        }

        public int GetFreeSpace()
        {
            return Math.Max(0, Capacity - Items.Count);
        }

        public void DropOnGround(Level level, float x, float y)
        {
            for (var i = 0; i < Items.Count; i++) ITEMS.ById[Items[i]].Drop(level, x, y, 1);

            Items.Clear();
        }
    }
}