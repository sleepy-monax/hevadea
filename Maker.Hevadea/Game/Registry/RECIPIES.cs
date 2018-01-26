using Maker.Hevadea.Game.Craftings;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Registry
{
    public static class RECIPIES
    {

        public static List<Recipe> HAND_CRAFTED = new List<Recipe>();

        public static void InitializeHandCraftedRecipe()
        {
            HAND_CRAFTED.Add(new Recipe(ITEMS.WOOD_PLANK, 2).AddCost(ITEMS.WOOD_LOG, 1));
            HAND_CRAFTED.Add(new Recipe(ITEMS.WOOD_STICK, 4).AddCost(ITEMS.WOOD_PLANK, 1));
            HAND_CRAFTED.Add(new Recipe(ITEMS.CHEST_ITEM, 1).AddCost(ITEMS.WOOD_PLANK, 8));
        }
    }
}
