using Hevadea.Game.Tiles;
using Hevadea.Game.Tiles.Renderers;
using Maker.Rise.Ressource;
using System.Collections.Generic;

namespace Hevadea.Game.Registry
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

            ROCK.AddTag(new Tags.Solide(), new Tags.Damage { ReplacementTile = DIRT });
            ROCK.AddTag(new Tags.Droppable((ITEMS.STONE, 2, 5), (ITEMS.COAL, 0, 3)));

            SAND.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            SAND.AddTag(new Tags.Droppable((ITEMS.SAND, 1, 2)));

            GRASS.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            GRASS.AddTag(new Tags.Spread { SpreadChance = 50, SpreadTo = { DIRT } });
            GRASS.AddTag(new Tags.Droppable((ITEMS.GRASS_PATCH, 1, 2)));

            WATER.AddTag(new Tags.Spread { SpreadChance = 1, SpreadTo = { VOID } });
            WATER.AddTag(new Tags.Liquide());
            WATER.AddTag(new Tags.Ground{ MoveSpeed = 0.5f });

            DIRT.AddTag(new Tags.Damage { ReplacementTile = VOID });

            WOOD_WALL.AddTag(new Tags.Solide());
            WOOD_WALL.AddTag(new Tags.Damage { ReplacementTile = DIRT });
            WOOD_WALL.AddTag(new Tags.Droppable((ITEMS.WOOD_WALL, 1, 2)));
            
            WOOD_FLOOR.AddTag(new Tags.Damage{ ReplacementTile = DIRT });
            WOOD_FLOOR.AddTag(new Tags.Droppable((ITEMS.WOOD_FLOOR, 1, 2)));
        }
    }
}