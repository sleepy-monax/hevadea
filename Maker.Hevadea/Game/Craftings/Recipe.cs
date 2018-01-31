using Maker.Hevadea.Game.Items;
using System.Collections.Generic;
using System.Linq;

namespace Maker.Hevadea.Game.Craftings
{
    public class Recipe
    {
        public  Item Result { get; }
        public  int Quantity { get; }
        public List<RecipeCost> Costs { get; }

        public Recipe(Item result, int quantity, params RecipeCost[] costs)
        {
            Result = result;
            Quantity = quantity;
            Costs = new List<RecipeCost>();
            Costs.AddRange(costs);
        }

        public Recipe AddCost(Item item, int quantity)
        {
            Costs.Add(new RecipeCost(item, quantity));

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
            foreach (var c in Costs)
            {
                i.Remove(c.Item, c.Count);
            }

            return true;
        }
    }
}
