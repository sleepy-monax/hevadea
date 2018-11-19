using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Items.Tags;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Registry
{
    public static class ITEMS
    {
        public static readonly List<Item> ById = new List<Item>();
        public static readonly Dictionary<string, Item> ByName = new Dictionary<string, Item>();

        public static Item BELT;
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
        public static Item WOOD_LOG;
        public static Item WOOD_PLANK;
        public static Item WOOD_STICK;
        public static Item WOOD_WALL;

        public static Item AXE;
        public static Item SWORD;
        public static Item PICKAXE;
        public static Item SHOVEl;
        public static Item HOE;

        public static void Initialize()
        {
            BELT = new Item("belt", new Sprite(Ressources.TileItems, new Point(9, 0)));
            CHEST = new Item("chest", new Sprite(Ressources.TileEntities, new Point(0, 1)));
            CRAFTING_BENCH = new Item("crafting_bench", new Sprite(Ressources.TileEntities, new Point(1, 0)));
            FURNACE = new Item("furnace", new Sprite(Ressources.TileEntities, new Point(1, 1)));

            LIGHTER = new Item("lighter", new Sprite(Ressources.TileEntities, new Point(4, 0)));
            GRASS_PATCH = new Item("grass_patch", new Sprite(Ressources.TileItems, new Point(7, 2)));
            COAL = new Item("coal", new Sprite(Ressources.TileItems, new Point(6, 2)));
            IRON_ORE = new Item("iron_ore", new Sprite(Ressources.TileItems, new Point(8, 0)));
            PINE_CONE = new Item("pine_cone", new Sprite(Ressources.TileItems, new Point(5, 2)));
            RAW_FISH = new Item("raw_fish", new Sprite(Ressources.TileEntities, new Point(11, 0)));
            SAND = new Item("sand", new Sprite(Ressources.TileItems, new Point(7, 3)));
            STONE = new Item("stone", new Sprite(Ressources.TileItems, new Point(7, 0)));
            TNT = new Item("tnt", new Sprite(Ressources.TileItems, new Point(0, 0)));
            TORCH = new Item("torch", new Sprite(Ressources.TileEntities, new Point(4, 0)));
            LANTERN = new Item("lantern", new Sprite(Ressources.TileEntities, new Point(4, 1)));
            WOOD_FLOOR = new Item("wood_floor", new Sprite(Ressources.TileItems, new Point(7, 5)));
            WOOD_LOG = new Item("wood_log", new Sprite(Ressources.TileItems, 6));
            WOOD_PLANK = new Item("wood_plank", new Sprite(Ressources.TileItems, new Point(6, 1)));
            WOOD_STICK = new Item("wood_stick", new Sprite(Ressources.TileItems, 5));
            WOOD_WALL = new Item("wood_wall", new Sprite(Ressources.TileItems, new Point(7, 4)));

            AXE = new Item("axe", new Sprite(Ressources.TileItems, new Point(0, 0)));
            PICKAXE = new Item("pickaxe", new Sprite(Ressources.TileItems, new Point(1, 0)));
            SHOVEl = new Item("shovel", new Sprite(Ressources.TileItems, new Point(2, 0)));
            SWORD = new Item("sword", new Sprite(Ressources.TileItems, new Point(3, 0)));
            HOE = new Item("hoe", new Sprite(Ressources.TileItems, new Point(4, 0)));
        }

        public static void AttachTags()
        {
            CHEST.AddTag(new PlaceEntity(ENTITIES.CHEST));
            CRAFTING_BENCH.AddTag(new PlaceEntity(ENTITIES.CRAFTING_BENCH));
            FURNACE.AddTag(new PlaceEntity(ENTITIES.FURNACE));
            TORCH.AddTag(new PlaceEntity(ENTITIES.TORCH));
            LANTERN.AddTag(new PlaceEntity(ENTITIES.LANTERN));

            WOOD_FLOOR.AddTag(new PlaceTile(TILES.WOOD_FLOOR) { CanBePlaceOn = { TILES.DIRT } });
            WOOD_WALL.AddTag(new PlaceTile(TILES.WOOD_WALL) { CanBePlaceOn = { TILES.DIRT } });
            GRASS_PATCH.AddTag(new PlaceTile(TILES.GRASS) { CanBePlaceOn = { TILES.DIRT } });
            SAND.AddTag(new PlaceTile(TILES.SAND) { CanBePlaceOn = { TILES.DIRT } });

            BELT.AddTag(new PlaceEntity(ENTITIES.BELT));
            TNT.AddTag(new PlaceEntity(ENTITIES.TNT));
            LIGHTER.AddTag(new ActionItemTag()
            {
                Action = (user, pos) =>
                {
                    foreach (var e in user.Level.QueryEntity(pos))
                    {
                        e.GetComponent<Flammable>()?.SetInFire();
                    }
                }
            });

            AXE.AddTag(new DamageTag()
            {
                PerEntityDamage =
                {
                    new GroupeDamage<EntityBlueprint>(ENTITIES.GROUPE_TREE, 2f),
                    new GroupeDamage<EntityBlueprint>(ENTITIES.GROUPE_CREATURE, 1.3f)
                }
            });

            SWORD.AddTag(new DamageTag()
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