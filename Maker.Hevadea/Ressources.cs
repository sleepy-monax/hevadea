using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea
{
    public static class Ressources
    {
        public static SpriteFont font_alagard;
        public static SpriteFont font_alagard_big;
        public static SpriteFont font_romulus;

        public static Texture2D img_icons;
        public static Texture2D img_tiles;
        public static Texture2D img_items;
        public static Texture2D img_entities;
        public static Texture2D img_creatures;

        public static Texture2D img_light;
        public static Texture2D img_shadow;

        public static SpriteSheet tile_tiles;
        public static SpriteSheet tile_icons;
        public static SpriteSheet tile_items;
        public static SpriteSheet tile_entities;
        public static SpriteSheet tile_creatures;

        public static Texture2D img_maker_logo;
        public static Texture2D img_engine_logo;
        public static Texture2D img_hevadea_logo;

        public static Texture2D img_forest_background;
        public static Texture2D img_forest_light;
        public static Texture2D img_forest_trees0;
        public static Texture2D img_forest_trees1;

        public static void Load()
        {
            font_alagard = Engine.Ressource.GetSpriteFont("alagard");
            font_alagard_big = Engine.Ressource.GetSpriteFont("alagard_big");
            font_romulus = Engine.Ressource.GetSpriteFont("romulus");

            img_tiles = Engine.Ressource.GetImage("tiles");
            img_items = Engine.Ressource.GetImage("items");
            img_icons = Engine.Ressource.GetImage("icons");
            img_entities = Engine.Ressource.GetImage("entities");
            img_creatures = Engine.Ressource.GetImage("creatures");

            img_light = Engine.Ressource.GetImage("light");
            img_shadow = Engine.Ressource.GetImage("shadow");

            img_maker_logo = Engine.Ressource.GetImage("logo/maker");
            img_engine_logo = Engine.Ressource.GetImage("logo/engine");
            img_hevadea_logo = Engine.Ressource.GetImage("logo/hevadea");

            img_forest_background = Engine.Ressource.GetImage("background/forest");
            img_forest_light = Engine.Ressource.GetImage("background/forest_light");
            img_forest_trees0 = Engine.Ressource.GetImage("background/forest_trees0");
            img_forest_trees1 = Engine.Ressource.GetImage("background/forest_trees1");

            tile_tiles = new SpriteSheet(img_tiles, new Point(32, 32));
            tile_icons = new SpriteSheet(img_icons, new Point(16, 16));
            tile_items = new SpriteSheet(img_items, new Point(16, 16));
            tile_entities = new SpriteSheet(img_entities, new Point(16, 16));
            tile_creatures = new SpriteSheet(img_creatures, new Point(48, 128));
        }
    }
}