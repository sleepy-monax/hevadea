using Hevadea.Entities;
using Hevadea.Entities.Blueprints;
using Hevadea.Items;
using System.Collections.Generic;

namespace Hevadea.Registry
{
    public static class ENTITIES
    {
        public static Groupe<EntityBlueprint> GROUPE_CREATURE;
        public static Groupe<EntityBlueprint> GROUPE_TREE;
        public static Groupe<EntityBlueprint> GROUPE_SAVE_EXCUDED;

        public static EntityBlueprint ITEM;

        public static EntityBlueprint BELT;
        public static EntityBlueprint BOAT;
        public static EntityBlueprint CHEST;
        public static EntityBlueprint CHIKEN;
        public static EntityBlueprint CRAFTING_BENCH;
        public static EntityBlueprint DOG;
        public static EntityBlueprint FISH;
        public static EntityBlueprint FLOWER;
        public static EntityBlueprint FURNACE;
        public static EntityBlueprint GRASS;
        public static EntityBlueprint PLAYER;
        public static EntityBlueprint STAIRES;
        public static EntityBlueprint TNT;
        public static EntityBlueprint TORCH;
        public static EntityBlueprint LANTERN;
        public static EntityBlueprint TREE;
        public static EntityBlueprint ZOMBIE;
        public static EntityBlueprint XPORB;

        public static void Initialize()
        {
            ITEM = RegisterEntityBlueprint(new GenericEntityBlueprint<ItemEntity>("item"));

            BELT = RegisterEntityBlueprint(new GenericEntityBlueprint<Belt>("belt"));
            BOAT = RegisterEntityBlueprint(new GenericEntityBlueprint<Boat>("boat"));
            CHEST = RegisterEntityBlueprint(new GenericEntityBlueprint<Chest>("chest"));
            CHIKEN = RegisterEntityBlueprint(new GenericEntityBlueprint<Chicken>("chiken"));
            CRAFTING_BENCH = RegisterEntityBlueprint(new GenericEntityBlueprint<Bench>("crafting_bench"));
            DOG = RegisterEntityBlueprint(new GenericEntityBlueprint<Dog>("dog"));
            FISH = RegisterEntityBlueprint(new GenericEntityBlueprint<Fish>("fish"));
            FLOWER = RegisterEntityBlueprint(new GenericEntityBlueprint<Flower>("flower"));
            FURNACE = RegisterEntityBlueprint(new GenericEntityBlueprint<Furnace>("furnace"));
            GRASS = RegisterEntityBlueprint(new GenericEntityBlueprint<Grass>("grass"));
            PLAYER = RegisterEntityBlueprint(new GenericEntityBlueprint<Player>("player"));
            STAIRES = RegisterEntityBlueprint(new GenericEntityBlueprint<Stairs>("staires"));
            TNT = RegisterEntityBlueprint(new GenericEntityBlueprint<TNT>("tnt"));
            TORCH = RegisterEntityBlueprint(new GenericEntityBlueprint<Torch>("torch"));
            LANTERN = RegisterEntityBlueprint(new GenericEntityBlueprint<Lamp>("lantern"));
            TREE = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTree>("tree"));
            ZOMBIE = RegisterEntityBlueprint(new GenericEntityBlueprint<Zombie>("zombie"));
            XPORB  = RegisterEntityBlueprint(new GenericEntityBlueprint<XpOrb>("xporb"));

            GROUPE_CREATURE = new Groupe<EntityBlueprint>("creature", CHIKEN, FISH, PLAYER, ZOMBIE);
            GROUPE_TREE = new Groupe<EntityBlueprint>("tree", TREE);
            GROUPE_SAVE_EXCUDED = new Groupe<EntityBlueprint>("save_excluded", PLAYER);
        }

        private static Dictionary<string, EntityBlueprint>
            _blueprintLibrary = new Dictionary<string, EntityBlueprint>();

        public static EntityBlueprint RegisterEntityBlueprint(EntityBlueprint blueprint)
        {
            if (_blueprintLibrary.ContainsKey(blueprint.Name))
                _blueprintLibrary[blueprint.Name] = blueprint;
            else
                _blueprintLibrary.Add(blueprint.Name, blueprint);

            return blueprint;
        }

        public static Entity Construct(string name)
        {
            return _blueprintLibrary.ContainsKey(name) ? _blueprintLibrary[name].Construct() : null;
        }

        public static EntityBlueprint GetBlueprint(string name)
        {
            return _blueprintLibrary.ContainsKey(name) ? _blueprintLibrary[name] : null;
        }
    }
}