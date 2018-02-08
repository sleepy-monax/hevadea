using Maker.Utils;

namespace Maker.Hevadea.Game.Registry
{
    public static class REGISTRY
    {
        public static void Initialize()
        {
            Logger.Log("Initializing game registery.");
            {
                TILES.Initialize();
                ITEMS.Initialize();
                RECIPIES.InitializeHandCraftedRecipe();
                GENERATOR.Initialize();
            }
        }
    }
}