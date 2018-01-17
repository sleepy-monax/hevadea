using System;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Items
{
    public class Inventory
    {
        public List<int> Items { get; set; }
        public int Capacity { get; private set; }

        public Inventory(int capacity = 256)
        {
            Items = new List<int>(capacity);
            Capacity = capacity;
        }


        public int Add(Item item, int quantity)
        {
            var count = quantity;
            for (int i = 0; i < quantity; i++)
            {
                if (Add(item)) count--;
            }

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
            var count = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == item.Id)
                {
                    count++;
                }
            }
            return count;
        }

        public void Remove(Item item, int quantity)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == item.Id && quantity > 0)
                {
                    Items.Remove(item.Id);
                    quantity--;
                    i--;
                }
            }
        }

        public int FreeSpace()
        {
            return Math.Max(0, Capacity - Items.Count);
        }
    }
}