﻿using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Items.Materials;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Registry
{
    public static class ITEMS
    {
        public static Item[] ById = new Item[256];

        public static Material WOOD_MATERIAL;
        public static Material IRON_MATERIAL;
        public static Material GOLD_MATERIAL;

        public static RessourceItem WOOD_LOG;
        public static RessourceItem WOOD_PLANK;
        public static RessourceItem WOOD_STICK;
        public static RessourceItem PINE_CONE;
        public static PlacableItem<ChestEntity> CHEST_ITEM;
        public static PlacableItem<TorchEntity> TORCH_ITEM;
        public static PlacableItem<CraftingBenchEntity> CRAFTINGBENCH_ITEM;
        public static RessourceItem STONE;
        public static RessourceItem COAL;

        public static void Initialize()
        {
            WOOD_MATERIAL = new BaseMaterial(2f);
            IRON_MATERIAL = new BaseMaterial(4f);
            GOLD_MATERIAL = new BaseMaterial(8f);

            WOOD_LOG   = new RessourceItem(0, "Wood Log", new Sprite(Ressources.tile_items, 6));
            WOOD_PLANK = new RessourceItem(1, "Wood Plank", new Sprite(Ressources.tile_items, new Point(6,1)));
            WOOD_STICK = new RessourceItem(2, "Wood Stick", new Sprite(Ressources.tile_items, 5));
            PINE_CONE  = new RessourceItem(3, "Pine Cone", new Sprite(Ressources.tile_items, new Point(5,2)));
            CHEST_ITEM = new PlacableItem<ChestEntity>(4, "Chest", new Sprite(Ressources.tile_entities, new Point(1, 1)));
            STONE = new RessourceItem(5, "Stone", new Sprite(Ressources.tile_items, new Point(7, 0)));
            COAL = new RessourceItem(6, "Coal", new Sprite(Ressources.tile_items, new Point(6, 2)));
            TORCH_ITEM = new PlacableItem<TorchEntity>(7, "Torch", new Sprite(Ressources.tile_entities, new Point(1, 3)));
            CRAFTINGBENCH_ITEM = new PlacableItem<CraftingBenchEntity>(9, "Bench", new Sprite(Ressources.tile_entities, new Point(2, 2)));
        }
    }
}
