using System.Collections.Generic;
using Hevadea.Entities;
using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Blueprints.Legacy;
using Hevadea.Items;

namespace Hevadea.Registry
{    
    public static class ENTITIES
    {
        public static EntityBlueprint ITEM;
        
        public static EntityBlueprint BELT;
        public static EntityBlueprint CHEST;
        public static EntityBlueprint CHIKEN;
        public static EntityBlueprint CRAFTING_BENCH;
        public static EntityBlueprint FISH;
        public static EntityBlueprint FLOWER;
        public static EntityBlueprint FURNACE;
        public static EntityBlueprint GRASS;
        public static EntityBlueprint PLAYER;
        public static EntityBlueprint STAIRES;
        public static EntityBlueprint TNT;
        public static EntityBlueprint TORCH;
        public static EntityBlueprint TREE;
        public static EntityBlueprint ZOMBIE;
        
        public static List<EntityBlueprint> SaveExcluded { get; set; } = new List<EntityBlueprint>();
        
        public static void Initialize()
        {
            ITEM = RegisterEntityBlueprint(new GenericEntityBlueprint<ItemEntity>("item"));
            
            BELT           = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityBelt>("belt"));
            CHEST          = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityChest>("chest"));
            CHIKEN         = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityChiken>("chiken"));
            CRAFTING_BENCH = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityCraftingBench>("crafting_bench"));
            FISH           = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityFish>("fish"));
            FLOWER         = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityFlower>("flower"));
            FURNACE        = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityFurnace>("furnace"));
            GRASS          = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityGrass>("grass"));
            PLAYER         = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityPlayer>("player"));
            STAIRES        = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityStairs>("staires"));
            TNT            = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTNT>("tnt"));
            TORCH          = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTorch>("torch"));
            TREE           = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTree>("tree"));
            ZOMBIE         = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityZombie>("zombie"));
            
            SaveExcluded.Add(PLAYER);
        }
        
        private static Dictionary<string, EntityBlueprint> _blueprintLibrary = new Dictionary<string, EntityBlueprint>();

        public static EntityBlueprint RegisterEntityBlueprint(EntityBlueprint blueprint)
        {
            if (_blueprintLibrary.ContainsKey(blueprint.Name))
            {
                _blueprintLibrary[blueprint.Name] = blueprint;
            }
            else
            {
                _blueprintLibrary.Add(blueprint.Name, blueprint);
            }

            return blueprint;
        }

        public static EntityBlueprint GetBlueprint(string name)
        {
            return _blueprintLibrary.ContainsKey(name) ? _blueprintLibrary[name] : null;
        }
    }
}