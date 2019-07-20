using Hevadea.Craftings;
using System.Collections.Generic;

namespace Hevadea.Registry
{
    public static class RECIPIES
    {
        public static readonly List<Recipe> BenchCrafted = new List<Recipe>();
        public static readonly List<Recipe> HandCrafted = new List<Recipe>();

        public static void InitializeHandCraftedRecipe()
        {
            BenchCrafted.Add(new Recipe(ITEMS.CHEST, 1).AddCost(ITEMS.MATERIAL_WOOD_PLANK, 8));
            BenchCrafted.Add(new Recipe(ITEMS.FURNACE, 1, new RecipeCost(ITEMS.STONE, 8)));
            BenchCrafted.Add(new Recipe(ITEMS.WOOD_FLOOR, 1, new RecipeCost(ITEMS.MATERIAL_WOOD_PLANK, 2)));
            BenchCrafted.Add(new Recipe(ITEMS.WOOD_WALL, 1, new RecipeCost(ITEMS.MATERIAL_WOOD_PLANK, 4)));
            BenchCrafted.Add(new Recipe(ITEMS.LANTERN, 1, new RecipeCost(ITEMS.TORCH, 1)).AddCost(ITEMS.IRON_ORE, 4));

            HandCrafted.Add(new Recipe(ITEMS.CRAFTING_BENCH, 1).AddCost(ITEMS.MATERIAL_WOOD_STICK, 4)
                .AddCost(ITEMS.MATERIAL_WOOD_PLANK, 4));
            HandCrafted.Add(new Recipe(ITEMS.LIGHTER, 1).AddCost(ITEMS.IRON_ORE, 1).AddCost(ITEMS.STONE, 1));
            HandCrafted.Add(new Recipe(ITEMS.TORCH, 4).AddCost(ITEMS.COAL, 1).AddCost(ITEMS.MATERIAL_WOOD_STICK, 1));
            HandCrafted.Add(new Recipe(ITEMS.MATERIAL_WOOD_PLANK, 2).AddCost(ITEMS.MATERIAL_WOOD_LOG, 1));
            HandCrafted.Add(new Recipe(ITEMS.MATERIAL_WOOD_STICK, 4).AddCost(ITEMS.MATERIAL_WOOD_PLANK, 1));
            HandCrafted.Add(new Recipe(ITEMS.BOAT, 4));

            HandCrafted.Add(new Recipe(ITEMS.WOOD_AXE, 1).AddCost(ITEMS.MATERIAL_WOOD_PLANK, 3).AddCost(ITEMS.MATERIAL_WOOD_STICK, 2));
            HandCrafted.Add(new Recipe(ITEMS.WOOD_SWORD, 1).AddCost(ITEMS.MATERIAL_WOOD_PLANK, 3).AddCost(ITEMS.MATERIAL_WOOD_STICK, 1));
        }
    }
}