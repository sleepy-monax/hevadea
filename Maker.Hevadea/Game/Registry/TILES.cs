using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.Game.Tiles.Renderers;
using Maker.Rise.Ressource;
using System.Collections.Generic;
using System.Threading;

namespace Maker.Hevadea.Game.Registry
{
    public static class TILES
    {
        public static List<Tile> ById = new List<Tile>();

        public static Tile VOID;
        public static Tile NULL;
        public static Tile GRASS;
        public static Tile SAND;
        public static Tile WATER;
        public static Tile ROCK;
        public static Tile WOOD_FLOOR;
        public static Tile WOOD_WALL;
        public static Tile DIRT;

        public static void Initialize()
        {
            ROCK       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 1)));
            GRASS      = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 2)));
            SAND       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 3)));
            WATER      = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 4)));
            WOOD_FLOOR = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 5)));
            WOOD_WALL  = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 6)));
            NULL       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 7)));
            VOID       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 8)));
            DIRT       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 9)));
        }

        public static void AttachTags()
        {
            VOID.AddTag(new Tags.Solide());

            ROCK.AddTag(new Tags.Solide(), new Tags.Breakable { ReplacementTile = DIRT });
            ROCK.AddTag(new Tags.Droppable((ITEMS.Stone, 2, 5), (ITEMS.Coal, 0, 3)));

            SAND.AddTag(new Tags.Breakable { ReplacementTile = DIRT });

            GRASS.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            GRASS.AddTag(new Tags.Spread { SpreadChance = 50, SpreadTo = { DIRT } });

            WATER.AddTag(new Tags.Spread { SpreadChance = 1, SpreadTo = { VOID } });
            WATER.AddTag(new Tags.Liquide());
            WATER.AddTag(new Tags.Ground{MoveSpeed = 0.5f});

            DIRT.AddTag(new Tags.Breakable { ReplacementTile = VOID });

            WOOD_WALL.AddTag(new Tags.Solide());
        }
    }
}