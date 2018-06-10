using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Entities.Blueprints.Legacy;
using Hevadea.GameObjects.Items;
using System.Collections.Generic;

namespace Hevadea.Registry
{
    public static class ENTITIES
    {
        public static BlueprintGroupe<EntityBlueprint> GROUPE_CREATURE;
        public static BlueprintGroupe<EntityBlueprint> GROUPE_TREE;
        public static BlueprintGroupe<EntityBlueprint> GROUPE_SAVE_EXCUDED;

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

        public static void Initialize()
        {
            ITEM = RegisterEntityBlueprint(new GenericEntityBlueprint<ItemEntity>("item"));

            BELT = RegisterEntityBlueprint(new GenericEntityBlueprint<Belt>("belt"));
            CHEST = RegisterEntityBlueprint(new GenericEntityBlueprint<Chest>("chest"));
            CHIKEN = RegisterEntityBlueprint(new GenericEntityBlueprint<Chicken>("chiken"));
            CRAFTING_BENCH = RegisterEntityBlueprint(new GenericEntityBlueprint<CraftingBench>("crafting_bench"));
            FISH = RegisterEntityBlueprint(new GenericEntityBlueprint<Fish>("fish"));
            FLOWER = RegisterEntityBlueprint(new GenericEntityBlueprint<Flower>("flower"));
            FURNACE = RegisterEntityBlueprint(new GenericEntityBlueprint<Furnace>("furnace"));
            GRASS = RegisterEntityBlueprint(new GenericEntityBlueprint<Grass>("grass"));
            PLAYER = RegisterEntityBlueprint(new GenericEntityBlueprint<Player>("player"));
            STAIRES = RegisterEntityBlueprint(new GenericEntityBlueprint<Stairs>("staires"));
            TNT = RegisterEntityBlueprint(new GenericEntityBlueprint<TNT>("tnt"));
            TORCH = RegisterEntityBlueprint(new GenericEntityBlueprint<Torch>("torch"));
            TREE = RegisterEntityBlueprint(new GenericEntityBlueprint<EntityTree>("tree"));
            ZOMBIE = RegisterEntityBlueprint(new GenericEntityBlueprint<Zombie>("zombie"));

            GROUPE_CREATURE = new BlueprintGroupe<EntityBlueprint>("creature") { Members = { CHIKEN, FISH, PLAYER, ZOMBIE } };
            GROUPE_TREE = new BlueprintGroupe<EntityBlueprint>("tree") { Members = { TREE } };
            GROUPE_SAVE_EXCUDED = new BlueprintGroupe<EntityBlueprint>("save_excluded") { Members = { PLAYER } };
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