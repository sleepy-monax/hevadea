using System.Collections.Generic;
using Maker.Hevadea.Game.Craftings;

namespace Maker.Hevadea.Game.Registry
{
    public static class RECIPIES
    {
        public static readonly List<Recipe> HandCrafted = new List<Recipe>();
        public static readonly List<Recipe> BenchCrafted = new List<Recipe>();

        public static void InitializeHandCraftedRecipe()
        {
            HandCrafted.Add(new Recipe(ITEMS.WoodPlank, 2).AddCost(ITEMS.WoodLog, 1));
            HandCrafted.Add(new Recipe(ITEMS.WoodStick, 4).AddCost(ITEMS.WoodPlank, 1));
            HandCrafted.Add(new Recipe(ITEMS.TorchItem, 4).AddCost(ITEMS.Coal, 1).AddCost(ITEMS.WoodStick, 1));
            HandCrafted.Add(new Recipe(ITEMS.CraftingbenchItem, 1).AddCost(ITEMS.WoodStick, 4).AddCost(ITEMS.WoodPlank, 4));
            
            BenchCrafted.Add(new Recipe(ITEMS.ChestItem, 1).AddCost(ITEMS.WoodPlank, 8));
            BenchCrafted.Add(new Recipe(ITEMS.FurnaceItem, 1, new RecipeCost(ITEMS.Stone, 8)));
        }
    }
}