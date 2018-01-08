using System.Collections.Generic;

namespace Maker.Hevadea.Game.Items
{
    public class Inventory
    {
        public int Capacity = 16;
        public List<Item> Items = new List<Item>();

        public void Remove<ItemType>(int quantity)
        {
        }

        public bool AddItem(Item item)
        {
            if (item is StackableItem s)
            {
                var stack = (StackableItem)Get(s);

                if (stack != null && stack.Count + s.Count <= stack.StackSize)
                {
                    stack.Count += s.Count;
                    return true;
                }

            }

            if (Items.Count == Capacity) return false;
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

        public int Count<T>() where T: Item
        {
            var count = 0;

            foreach (var item in Items)
            {
                if (item is T)
                {
                    count++;
                }
            }

            return count;
        }



        public int Count(Item item)
        {
            var count = 0;

            foreach (var i in Items)
            {
                if (i.GetType() == item.GetType())
                {
                    count++;
                }
            }

            return count;
        }

        public int Count()
        {
            var count = 0;

            foreach (var i in Items)
            {

                count++;

            }

            return count;
        }
    }
}