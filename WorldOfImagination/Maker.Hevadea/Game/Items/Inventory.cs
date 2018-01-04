using System.Collections.Generic;

namespace Maker.Hevadea.Game.Items
{
    public class Inventory
    {
        public int capacity = 16;
        public List<Item> Items = new List<Item>();

        public void Remove<ItemType>(int quantity)
        {

        }

        public bool AddItem(Item item)
        {
            if (item is StackableItem s)
            {
                if (Items.Count != capacity || Contain(s))
                {
                    if (s.Count < s.StackSize)
                    {
                        s.Count++;
                        return true;
                    }
                }
            }

            if (Items.Count == capacity)
            {
                return false;
            }

            Items.Add(item);
            return true;
        }

        public Item Get(Item itemType)
        {
            foreach (var item in Items)
            {
                if (item.GetType() == itemType.GetType()) return item;
            }

            return null;
        }

        public bool Contain(Item itemType)
        {
            foreach (var item in Items)
            {
                if (item.GetType() == itemType.GetType()) return true;
            }

            return false;
        }

        public int Count<ItemType>()
        {
            int count = 0;

            foreach (var item in Items)
            {
                if (typeof(Item) is ItemType)
                {
                    count++;
                }
            }

            return count;
        }
    }
}