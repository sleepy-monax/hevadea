using System.Collections.Generic;
using Hevadea.GameObjects.Items;

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

        public virtual bool Craft(ItemStorage i)
        {
            if (CanBeCrafted(i) && i.GetFreeSpace() >= Quantity)
            {
                i.Add(Result, Quantity);
                foreach (var c in Costs) i.Remove(c.Item, c.Count);

                return true;
            }

            return false;
        }
    }
}