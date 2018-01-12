using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Items;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Craftings
{
    public class Recipe
    {
        public Item Result;
        public int Quantity;
        List<RecipeCost> Costs = new List<RecipeCost>();

        public Recipe(Item result, int quantity)
        {
            Result = result;
            Quantity = quantity;
        }

        public Recipe AddCost(Item item, int quantity)
        {
            Costs.Add(new RecipeCost(item, quantity));

            return this;
        }

        public virtual bool CanBeCrafted(Inventory i)
        {
            var canBeCrafted = true;
            foreach (var c in Costs)
            {
                canBeCrafted = canBeCrafted && i.Count(c.Item) >= c.Count;
            }
            return canBeCrafted;
        }

        public virtual bool Craft(Inventory i)
        {
            if (CanBeCrafted(i) && i.FreeSpace() >= Quantity)
            {

                i.Add(Result, Quantity);

                foreach (var c in Costs)
                {
                    i.Remove(c.Item, c.Count);
                }

                return true;
            }

            return false;
        }
    }
}
