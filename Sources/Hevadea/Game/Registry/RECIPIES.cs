using Hevadea.Game.Craftings;
using System.Collections.Generic;

namespace Hevadea.Game.Registry
{
    public static class RECIPIES
    {
        public static readonly List<Recipe> HandCrafted = new List<Recipe>();
        public static readonly List<Recipe> BenchCrafted = new List<Recipe>();

        public static void InitializeHandCraftedRecipe()
        {
            HandCrafted.Add(new Recipe(ITEMS.WOOD_PLANK, 2).AddCost(ITEMS.WOOD_LOG, 1));
            HandCrafted.Add(new Recipe(ITEMS.WOOD_STICK, 4).AddCost(ITEMS.WOOD_PLANK, 1));
            HandCrafted.Add(new Recipe(ITEMS.TORCH, 4).AddCost(ITEMS.COAL, 1).AddCost(ITEMS.WOOD_STICK, 1));
            HandCrafted.Add(new Recipe(ITEMS.CRAFTING_BENCH, 1).AddCost(ITEMS.WOOD_STICK, 4).AddCost(ITEMS.WOOD_PLANK, 4));
            HandCrafted.Add(new Recipe(ITEMS.BELT, 4));
            HandCrafted.Add(new Recipe(ITEMS.TNT, 4));
            HandCrafted.Add(new Recipe(ITEMS.LIGHTER, 1).AddCost(ITEMS.IRON_ORE, 1).AddCost(ITEMS.STONE, 1));

            BenchCrafted.Add(new Recipe(ITEMS.CHEST, 1).AddCost(ITEMS.WOOD_PLANK, 8));
            BenchCrafted.Add(new Recipe(ITEMS.FURNACE, 1, new RecipeCost(ITEMS.STONE, 8)));
            BenchCrafted.Add(new Recipe(ITEMS.WOOD_FLOOR, 1, new RecipeCost(ITEMS.WOOD_PLANK, 2)));
            BenchCrafted.Add(new Recipe(ITEMS.WOOD_WALL, 1, new RecipeCost(ITEMS.WOOD_PLANK, 4)));

        }
    }
}