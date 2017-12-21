using Maker.Rise;
using Maker.Rise.GameComponent.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfImagination.Game
{
    public static class Ressources
    {
        public static Texture2D img_tiles;
        public static Texture2D img_items;
        public static Texture2D img_entities;

        public static TileSheet tile_tiles;
        public static TileSheet tile_items;
        public static TileSheet tile_entities;

        public static void Load(WorldOfImaginationGame Game)
        {
            img_tiles = Game.Ressource.GetImage("tiles");
            img_items = Game.Ressource.GetImage("items");
            img_entities = Game.Ressource.GetImage("entities");

            tile_tiles = new TileSheet(img_tiles, new Point(32, 32));
            tile_items = new TileSheet(img_items, new Point(32, 32));
            tile_entities = new TileSheet(img_entities, new Point(32, 32));
        }

    }
}
