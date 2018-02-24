using Hevadea.Game.Items;
using Hevadea.Game.Items.Materials;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Game.Registry
{
    public static class ITEMS
    {
        public static readonly List<Item> ById = new List<Item>();

        public static Material WoodMaterial;
        public static Material IronMaterial;
        public static Material GoldMaterial;

        public static Item WOOD_LOG;
        public static Item WOOD_PLANK;
        public static Item WOOD_STICK;
        public static Item PINE_CONE;
        public static Item STONE;
        public static Item COAL;

        public static Item CHEST;
        public static Item TORCH;
        public static Item CRAFTING_BENCH;
        public static Item FURNACE;

        public static void Initialize()
        {
            WoodMaterial = new BaseMaterial(2f);
            IronMaterial = new BaseMaterial(4f);
            GoldMaterial = new BaseMaterial(8f);

            COAL       = new RessourceItem("coal",       new Sprite(Ressources.TileItems, new Point(6, 2)));
            STONE      = new RessourceItem("stone",      new Sprite(Ressources.TileItems, new Point(7, 0)));
            PINE_CONE  = new RessourceItem("pine_cone",  new Sprite(Ressources.TileItems, new Point(5, 2)));
            WOOD_LOG   = new RessourceItem("wood_log",   new Sprite(Ressources.TileItems, 6));
            WOOD_PLANK = new RessourceItem("wood_plank", new Sprite(Ressources.TileItems, new Point(6, 1)));
            WOOD_STICK = new RessourceItem("wood_stick", new Sprite(Ressources.TileItems, 5));

            CHEST          = new PlacableItem("chest",          ENTITIES.CHEST,          new Sprite(Ressources.TileEntities, new Point(0, 1)));
            CRAFTING_BENCH = new PlacableItem("crafting_bench", ENTITIES.CRAFTING_BENCH, new Sprite(Ressources.TileEntities, new Point(1, 0)));
            FURNACE        = new PlacableItem("furnace",        ENTITIES.FURNACE,        new Sprite(Ressources.TileEntities, new Point(1, 1)));
            TORCH          = new PlacableItem("torch",          ENTITIES.TORCH,          new Sprite(Ressources.TileEntities, new Point(4, 0)));
        }
    }
}