using Maker.Hevadea.Game.Craftings;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Registry
{
    public static class RECIPIES
    {

        public static readonly List<Recipe> HandCrafted = new List<Recipe>();

        public static void InitializeHandCraftedRecipe()
        {
            HandCrafted.Add(new Recipe(ITEMS.WoodPlank, 2).AddCost(ITEMS.WoodLog, 1));
            HandCrafted.Add(new Recipe(ITEMS.WoodStick, 4).AddCost(ITEMS.WoodPlank, 1));
            HandCrafted.Add(new Recipe(ITEMS.ChestItem, 1).AddCost(ITEMS.WoodPlank, 8));
            HandCrafted.Add(new Recipe(ITEMS.TorchItem, 4).AddCost(ITEMS.Coal, 1).AddCost(ITEMS.WoodStick, 1));
            HandCrafted.Add(new Recipe(ITEMS.CraftingbenchItem, 1).AddCost(ITEMS.WoodStick, 4).AddCost(ITEMS.WoodPlank, 4));
        }
    }
}
