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