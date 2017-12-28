using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea
{
    public static class Ressources
    {
        public static Texture2D img_tiles;
        public static Texture2D img_items;
        public static Texture2D img_entities;

        public static SpriteSheet tile_tiles;
        public static SpriteSheet tile_items;
        public static SpriteSheet tile_entities;

        public static void Load()
        {
            img_tiles = Engine.Ressource.GetImage("tiles");
            img_items = Engine.Ressource.GetImage("items");
            img_entities = Engine.Ressource.GetImage("entities");

            tile_tiles = new SpriteSheet(img_tiles, new Point(32, 32));
            tile_items = new SpriteSheet(img_items, new Point(32, 32));
            tile_entities = new SpriteSheet(img_entities, new Point(48, 64));
        }
    }
}
