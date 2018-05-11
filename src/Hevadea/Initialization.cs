using Hevadea.GameObjects;
using Hevadea.Registry;

namespace Hevadea
{
    public class Initialization
    {
        public void Initialize()
        {
            PreInit();
            Init();
            PostInit();
        }
        
        private static void PreInit()
        {
            Ressources.Load();
        }

        private static void Init()
        {
            TILES.Initialize();
            EntityFactory.Initialize();
            ITEMS.Initialize();
            
            RECIPIES.InitializeHandCraftedRecipe();
            
            LEVELS.Initialize();
            
            GENERATOR.Initialize();
        }

        private static void PostInit()
        {
            TILES.AttachRender();
            
            TILES.AttachTags();
            ITEMS.AttachTags();
        }
    }
}