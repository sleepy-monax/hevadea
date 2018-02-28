using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Entities.Furnitures;
using System.Collections.Generic;

namespace Hevadea.Game.Registry
{
    public class GenericEntityBlueprint<T> : EntityBlueprint where T : Entity, new()
    {
        public GenericEntityBlueprint(string name) : base(name){}
        public override Entity Build()
        {
            return new T{Blueprint = this};
        }
    }
    
    public class EntityBlueprint
    {
        public string Name { get; }

        public EntityBlueprint(string name)
        {
            Name = name;
        }
        
        public virtual Entity Build()
        {
            return new Entity{Blueprint = this};
        }
    }
    
    public static class ENTITIES
    {
        public static EntityBlueprint PLAYER;
        public static EntityBlueprint ZOMBIE;

        public static EntityBlueprint GRASS;
        public static EntityBlueprint TREE;

        public static EntityBlueprint CHEST;
        public static EntityBlueprint CRAFTING_BENCH;
        public static EntityBlueprint FURNACE;
        public static EntityBlueprint STAIRES;
        public static EntityBlueprint TORCH;

        public static EntityBlueprint BELT;
        
        public static void Initialize()
        {
            // Creatures
            PLAYER = RegisterEntityBlueprint(new GenericEntityBlueprint<PlayerEntity>("player"));
            ZOMBIE = RegisterEntityBlueprint(new GenericEntityBlueprint<ZombieEntity>("zombie"));

            // Plants
            GRASS = RegisterEntityBlueprint(new GenericEntityBlueprint<GrassEntity>("grass"));
            TREE = RegisterEntityBlueprint(new GenericEntityBlueprint<TreeEntity>("tree"));
            
            // Furnitures
            CHEST          = RegisterEntityBlueprint(new GenericEntityBlueprint<ChestEntity>("chest"));
            CRAFTING_BENCH = RegisterEntityBlueprint(new GenericEntityBlueprint<CraftingBenchEntity>("crafting_bench"));
            FURNACE        = RegisterEntityBlueprint(new GenericEntityBlueprint<FurnaceEntity>("furnace"));
            STAIRES        = RegisterEntityBlueprint(new GenericEntityBlueprint<StairsEntity>("staires"));
            TORCH          = RegisterEntityBlueprint(new GenericEntityBlueprint<TorchEntity>("torch"));

            BELT = RegisterEntityBlueprint(new GenericEntityBlueprint<BeltEntity>("belt"));
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