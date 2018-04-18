using Hevadea.Framework.Utils;
using Hevadea.GameObjects;

namespace Hevadea.Registry
{
    public static class REGISTRY
    {
        public static void Initialize()
        {
            Logger.Log("Initializing game registery.");
            
            TILES.Initialize();
            EntityFactory.Initialize();
            ITEMS.Initialize();
            RECIPIES.InitializeHandCraftedRecipe();
            LEVELS.Initialize();
            GENERATOR.Initialize();
            TILES.AttachRender();
            TILES.AttachTags();
            ITEMS.AttachTags();
        }
    }
}