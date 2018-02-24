using Hevadea.Game.Items;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Game.Craftings
{
    public class Recipe
    {
        public Item Result { get; set; }
        public int Quantity { get; set; }
        public double CraftingTime { get; set; } = 0f;
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

        public Recipe SetCraftingTime(double time)
        {
            CraftingTime = time;
            return this;
        }
        
        public bool CanBeCrafted(ItemStorage i)
        {
            return Costs.Aggregate(true, (current, c) => current && i.Count(c.Item) >= c.Count);
        }

        public bool Craft(ItemStorage i)
        {
            if (!CanBeCrafted(i) || i.GetFreeSpace() < Quantity) return false;

            i.Add(Result, Quantity);
            foreach (var c in Costs) i.Remove(c.Item, c.Count);

            return true;
        }
    }
}