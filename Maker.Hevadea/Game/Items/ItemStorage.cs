using System;
using System.Collections.Generic;
using Maker.Hevadea.Game.Registry;

namespace Maker.Hevadea.Game.Items
{    
    public class ItemStorage
    {
        public Dictionary<int, int> Items { get; set; }
        public int Capacity { get; }
        
        public ItemStorage(int capacity = 256)
        {
            Items = new Dictionary<int, int>();
            Capacity = capacity;
        }

        public int Add(Item item, int quantity)
        {
            if (item == null) return quantity;
            
            var count = quantity;
            for (var i = 0; i < quantity; i++)
                if (Add(item))
                    count--;

            return count;
        }

        public bool Add(Item item)
        {
            if (item == null) return false;
            
            if (HasFreeSlot())
            {
                if (Items.ContainsKey(item.Id))
                {
                    Items[item.Id] = Items[item.Id] + 1;
                }
                else
                {
                    Items.Add(item.Id, 1);    
                }
                
                return true;
            }

            return false;
        }

        public bool HasFreeSlot()
        {
            return Count() < Capacity;
        }

        public int Count()
        {
            int result = 0;

            foreach (var i in Items)
            {
                result += i.Value;
            }

            return result;
        }
        
        public int Count(Item item)
        {
            if (item == null) return 0;
            
            if (Items.ContainsKey(item.Id))
            {
                return Items[item.Id];
            }

            return 0;
        }

        public void Remove(Item item, int quantity)
        {
            if (item == null) return;
            if (Items.ContainsKey(item.Id))
            {
                Items[item.Id] = Math.Max(Items[item.Id] - quantity, 0);

                if (Items[item.Id] == 0)
                {
                    Items.Remove(item.Id);
                }
            }
        }

        public int GetFreeSpace()
        {
            return Math.Max(0, Capacity - Count());
        }

        public void DropOnGround(Level level, float x, float y)
        {
            foreach (var i in Items)
            {
                ITEMS.ById[i.Key].Drop(level, x, y, i.Value);
            }

            Items.Clear();
        }
    }
}