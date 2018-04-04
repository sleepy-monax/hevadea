using System.Collections.Generic;
using Hevadea.Entities;
using Hevadea.Entities.Blueprints;
using Hevadea.Items;

namespace Hevadea.Registry
{    
    public static class ENTITIES
    {
        public static EntityBlueprint ITEM;
        
        public static EntityBlueprint PLAYER;
        public static EntityBlueprint ZOMBIE;
        public static EntityBlueprint FISH;
        public static EntityBlueprint CHIKEN;

        public static EntityBlueprint GRASS;
        public static EntityBlueprint TREE;

        public static EntityBlueprint CHEST;
        public static EntityBlueprint CRAFTING_BENCH;
        public static EntityBlueprint FURNACE;
        public static EntityBlueprint STAIRES;
        public static EntityBlueprint TORCH;

        public static EntityBlueprint BELT;
        public static EntityBlueprint TNT;
        
        public static List<EntityBlueprint> SaveExcluded { get; set; } = new List<EntityBlueprint>();
        
        public static void Initialize()
        {
            ITEM = RegisterEntityBlueprint(new GenericEntityBlueprint<ItemEntity>("item"));
            
            // Creatures
            PLAYER = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityPlayer>("player"));
            ZOMBIE = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityZombie>("zombie"));
            FISH = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityFish>("fish"));
            CHIKEN = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityChiken>("chiken"));

            // Plants
            GRASS = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityGrass>("grass"));
            TREE = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTree>("tree"));
            
            // Furnitures
            CHEST          = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityChest>("chest"));
            CRAFTING_BENCH = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityCraftingBench>("crafting_bench"));
            FURNACE        = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityFurnace>("furnace"));
            STAIRES        = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityStairs>("staires"));
            TORCH          = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTorch>("torch"));

            BELT = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityBelt>("belt"));
            TNT = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTNT>("tnt"));
            
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