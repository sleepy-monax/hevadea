using Hevadea.Entities.Blueprints;
using Hevadea.Items;
using Hevadea.Items.Tags;
using System.Collections.Generic;
using Hevadea.Entities.Components;

namespace Hevadea.Registry
{
    public static class ITEMS
    {
        public static readonly List<Item> ById = new List<Item>();
        public static readonly Dictionary<string, Item> ByName = new Dictionary<string, Item>();

        public static Item BELT;
        public static Item BOAT;
        public static Item CHEST;
        public static Item COAL;
        public static Item CRAFTING_BENCH;
        public static Item FURNACE;
        public static Item GRASS_PATCH;
        public static Item IRON_ORE;
        public static Item LIGHTER;
        public static Item PINE_CONE;
        public static Item RAW_FISH;
        public static Item SAND;
        public static Item STONE;
        public static Item TNT;
        public static Item TORCH;
        public static Item LANTERN;
        public static Item WOOD_FLOOR;
        public static Item MATERIAL_WOOD_LOG;
        public static Item MATERIAL_WOOD_PLANK;
        public static Item MATERIAL_WOOD_STICK;
        public static Item WOOD_WALL;

        public static Item WOOD_AXE;
        public static Item WOOD_SWORD;
        public static Item WOOD_PICKAXE;
        public static Item WOOD_SHOVEL;
        public static Item WOOD_HOE;

        public static void Initialize()
        {
            BELT = new Item("belt", Resources.Sprites["item/belt"]);
            BOAT = new Item("boat", Resources.Sprites["item/boat"]);
            CHEST = new Item("chest", Resources.Sprites["item/chest"]);
            CRAFTING_BENCH = new Item("bench", Resources.Sprites["item/bench"]);
            FURNACE = new Item("furnace", Resources.Sprites["item/furnace"]);

            LIGHTER = new Item("lighter", Resources.Sprites["item/lighter"]);
            GRASS_PATCH = new Item("floor_grass", Resources.Sprites["item/floor_grass"]);
            COAL = new Item("ore_coal", Resources.Sprites["item/ore_coal"]);
            IRON_ORE = new Item("ore_iron", Resources.Sprites["item/ore_iron"]);
            PINE_CONE = new Item("seed_pine", Resources.Sprites["item/seed_pine"]);
            RAW_FISH = new Item("raw_fish", Resources.Sprites["item/raw_fish"]);
            SAND = new Item("floor_sand", Resources.Sprites["item/floor_sand"]);
            TNT = new Item("tnt", Resources.Sprites["item/tnt"]);
            TORCH = new Item("torch", Resources.Sprites["item/torch"]);
            LANTERN = new Item("lamp", Resources.Sprites["item/lamp"]);

            STONE = new Item("material_stone", Resources.Sprites["item/material_stone"]);

            MATERIAL_WOOD_LOG = new Item("material_wood_log", Resources.Sprites["item/material_wood_log"]);
            MATERIAL_WOOD_PLANK = new Item("material_wood_plank", Resources.Sprites["item/material_wood_plank"]);
            MATERIAL_WOOD_STICK = new Item("material_wood_stick", Resources.Sprites["item/material_wood_stick"]);

            WOOD_FLOOR = new Item("floor_wood", Resources.Sprites["item/floor_wood"]);
            WOOD_WALL = new Item("wall_wood", Resources.Sprites["item/wall_wood"]);
            WOOD_AXE = new Item("axe_wood", Resources.Sprites["item/axe_wood"]);
            WOOD_PICKAXE = new Item("pickaxe_wood", Resources.Sprites["item/pickaxe_wood"]);
            WOOD_SHOVEL = new Item("shovel_wood", Resources.Sprites["item/shovel_wood"]);
            WOOD_SWORD = new Item("sword_wood", Resources.Sprites["item/sword_wood"]);
            WOOD_HOE = new Item("hoe_wood", Resources.Sprites["item/hoe_wood"]);
        }

        public static void AttachTags()
        {
            CHEST.AddTag(new PlaceEntity(ENTITIES.CHEST));
            CRAFTING_BENCH.AddTag(new PlaceEntity(ENTITIES.CRAFTING_BENCH));
            FURNACE.AddTag(new PlaceEntity(ENTITIES.FURNACE));
            TORCH.AddTag(new PlaceEntity(ENTITIES.TORCH));
            LANTERN.AddTag(new PlaceEntity(ENTITIES.LANTERN));

            WOOD_FLOOR.AddTag(new PlaceTile(TILES.WOOD_FLOOR) {CanBePlaceOn = {TILES.DIRT}});
            WOOD_WALL.AddTag(new PlaceTile(TILES.WOOD_WALL) {CanBePlaceOn = {TILES.DIRT}});
            GRASS_PATCH.AddTag(new PlaceTile(TILES.GRASS) {CanBePlaceOn = {TILES.DIRT}});
            SAND.AddTag(new PlaceTile(TILES.SAND) {CanBePlaceOn = {TILES.DIRT}});

            BOAT.AddTag(new PlaceEntity(ENTITIES.BOAT));
            BELT.AddTag(new PlaceEntity(ENTITIES.BELT));
            TNT.AddTag(new PlaceEntity(ENTITIES.TNT));
            LIGHTER.AddTag(new ActionItemTag()
            {
                Action = (user, pos) =>
                {
                    foreach (var e in user.Level.QueryEntity(pos)) e.GetComponent<ComponentFlammable>()?.SetInFire();
                }
            });

            WOOD_AXE.AddTag(new DamageTag()
            {
                PerEntityDamage =
                {
                    new GroupeDamage<EntityBlueprint>(ENTITIES.GROUPE_TREE, 2f),
                    new GroupeDamage<EntityBlueprint>(ENTITIES.GROUPE_CREATURE, 1.3f)
                }
            });

            WOOD_SWORD.AddTag(new DamageTag()
            {
                PerEntityDamage =
                {
                    new GroupeDamage<EntityBlueprint>(ENTITIES.GROUPE_TREE, 1.1f),
                    new GroupeDamage<EntityBlueprint>(ENTITIES.GROUPE_CREATURE, 2f)
                }
            });
        }
    }
}