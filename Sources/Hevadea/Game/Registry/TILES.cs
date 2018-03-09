using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Items;
using Hevadea.Game.Tiles;
using Hevadea.Game.Tiles.Renderers;
using System.Collections.Generic;

namespace Hevadea.Game.Registry
{
    public static class TILES
    {
        public static List<Tile> ById = new List<Tile>();

        public static Tile VOID;
        public static Tile GRASS;
        public static Tile SAND;
        public static Tile WATER;
        public static Tile ROCK;
        public static Tile WOOD_FLOOR;
        public static Tile WOOD_WALL;
        public static Tile DIRT;
        public static Tile IRON_ORE;

        public static void Initialize()
        {
            ROCK       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 1)));
            GRASS      = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 2)));
            SAND       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 3)));
            WATER      = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 4)));
            WOOD_FLOOR = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 5)));
            WOOD_WALL  = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 6)));
            VOID       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 8)));
            DIRT       = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 9)));
            IRON_ORE   = new Tile(new CompositConectedTileRender(new Sprite(Ressources.TileTiles, 10)));
        }

        public static void AttachTags()
        {
            VOID.AddTag(new Tags.Solide());

            ROCK.AddTag(new Tags.Solide(), new Tags.Damage { ReplacementTile = DIRT });
            ROCK.AddTag(new Tags.Droppable(new Drop(ITEMS.STONE,1f, 2, 4), new Drop(ITEMS.COAL,1f, 0, 2)));

            SAND.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            SAND.AddTag(new Tags.Droppable(new Drop(ITEMS.SAND, 1f, 1, 1)));

            GRASS.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            GRASS.AddTag(new Tags.Spread { SpreadChance = 50, SpreadTo = { DIRT } });
            GRASS.AddTag(new Tags.Droppable(new Drop(ITEMS.GRASS_PATCH, 1f, 1, 1)));

            WATER.AddTag(new Tags.Spread { SpreadChance = 1, SpreadTo = { VOID } });
            WATER.AddTag(new Tags.Liquide());
            WATER.AddTag(new Tags.Ground{ MoveSpeed = 0.5f });

            DIRT.AddTag(new Tags.Damage { ReplacementTile = VOID });

            WOOD_WALL.AddTag(new Tags.Solide());
            WOOD_WALL.AddTag(new Tags.Damage { ReplacementTile = DIRT });
            WOOD_WALL.AddTag(new Tags.Droppable(new Drop(ITEMS.WOOD_WALL, 1f, 1, 1)));

            WOOD_FLOOR.AddTag(new Tags.Damage { ReplacementTile = DIRT });
            WOOD_FLOOR.AddTag(new Tags.Droppable(new Drop(ITEMS.WOOD_FLOOR, 1f, 1, 1)));

            IRON_ORE.AddTag(new Tags.Solide(), new Tags.Damage { ReplacementTile = DIRT });
            IRON_ORE.AddTag(new Tags.Droppable(new Drop(ITEMS.IRON_ORE, 1f, 1, 2)));

        }
    }
}