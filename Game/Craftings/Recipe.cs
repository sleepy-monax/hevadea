using Hevadea.Items;
using System.Collections.Generic;

namespace Hevadea.Craftings
{
    public class Recipe
    {
        public Item Result { get; set; }
        public int Quantity { get; set; }
        public List<RecipeCost> Costs { get; set; } = new List<RecipeCost>();

        public Recipe(Item result, int quantity)
        {
            Result = result;
            Quantity = quantity;
        }

        public Recipe(Item result, int quantity, params RecipeCost[] costs)
        {
            Result = result;
            Quantity = quantity;
            Costs.AddRange(costs);
        }

        public Recipe AddCost(Item item, int quantity)
        {
            Costs.Add(new RecipeCost(item, quantity));
            return this;
        }

        public virtual bool CanBeCrafted(ItemStorage inventory)
        {
            var result = true;

            foreach (var cost in Costs)
                result &= inventory.Count(cost.Item) >= cost.Count;

            return result;
        }

        public virtual bool Craft(ItemStorage inventory)
        {
            return Craft(inventory, inventory);
        }

        public virtual bool Craft(ItemStorage materialStorage, ItemStorage destinationStorage)
        {
            if (CanBeCrafted(materialStorage) && destinationStorage.GetFreeSpace() >= Quantity)
            {
                destinationStorage.Add(Result, Quantity);
                foreach (var c in Costs) materialStorage.Remove(c.Item, c.Count);

                return true;
            }

            return false;
        }
    }
}