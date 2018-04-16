using System.Collections.Generic;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Blueprints.Legacy;
using Hevadea.GameObjects.Items;

namespace Hevadea.Registry
{

    public class EntityGroupe
    {
        public string Name { get; }
        public List<EntityBlueprint> Members { get; set; } = new List<EntityBlueprint>();
        
        public EntityGroupe(string name)
        {
            Name = name;
        }
    }
    
    
    public static class ENTITIES
    {
        public static EntityGroupe GROUPE_CREATURE;
        public static EntityGroupe GROUPE_TREE;
        
        
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
        
        //Todo replace with a EntityGroupe.
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
            
            GROUPE_CREATURE = new EntityGroupe("creature"){ Members = { CHIKEN, FISH, PLAYER, ZOMBIE } };
            GROUPE_TREE = new EntityGroupe("tree"){ Members = { TREE } };
            
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