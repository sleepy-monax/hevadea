using Hevadea.Framework.Graphic;
using Hevadea.Items;
using Hevadea.Tiles;
using Hevadea.Tiles.Components;
using Hevadea.Tiles.Renderers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Registry
{
    public static class TILES
    {
        private static Dictionary<string, Tile> _tiles = new Dictionary<string, Tile>();

        public static Groupe<Tile> GROUPE_SOIL;
        public static Groupe<Tile> GROUPE_ROCK;
        public static Groupe<Tile> GROUPE_WOOD;

        public static Tile DIRT;
        public static Tile GRASS;
        public static Tile IRON_ORE;
        public static Tile ROCK;
        public static Tile SAND;
        public static Tile VOID;
        public static Tile WATER;
        public static Tile WOOD_FLOOR;
        public static Tile WOOD_WALL;

        public static Tile RegisterTile(Tile tile)
        {
            if (_tiles.ContainsKey(tile.Name))
                _tiles[tile.Name] = tile;
            else
                _tiles.Add(tile.Name, tile);

            return tile;
        }

        public static Tile GetTile(string name)
        {
            if (_tiles.TryGetValue(name, out var tile))
                return tile;

            return null;
        }

        public static List<Tile> GetTiles()
        {
            return _tiles.Values.ToList();
        }

        public static void Initialize()
        {
            DIRT = RegisterTile(new Tile("dirt", new TileRenderComposite(new Sprite(Resources.TileTiles, 9)),
                Color.Brown));
            GRASS = RegisterTile(new Tile("grass", Color.Green));
            IRON_ORE = RegisterTile(new Tile("iron_ore", new TileRenderComposite(new Sprite(Resources.TileTiles, 10)),
                Color.Orange));
            ROCK = RegisterTile(new Tile("rock"));
            SAND = RegisterTile(new Tile("sand", Color.Yellow));
            VOID = RegisterTile(new Tile("void"));
            WATER = RegisterTile(new Tile("water", Color.Blue));
            WOOD_FLOOR = RegisterTile(new Tile("wood_floor",
                new TileRenderComposite(new Sprite(Resources.TileTiles, 5)), Color.SandyBrown));
            WOOD_WALL = RegisterTile(new Tile("wood_wall", new TileRenderComposite(new Sprite(Resources.TileTiles, 6)),
                Color.SandyBrown));

            GROUPE_ROCK = new Groupe<Tile>("rock", IRON_ORE, ROCK);
            GROUPE_SOIL = new Groupe<Tile>("soil", DIRT);
            GROUPE_WOOD = new Groupe<Tile>("wood", WOOD_FLOOR, WOOD_WALL);
        }

        public static void AttachRender()
        {
            GRASS.Render = new TileRenderSpriteSheet(Resources.TileGrass);
            IRON_ORE.Render = new TileRenderComposite(new Sprite(Resources.TileTiles, 10)) {ConnectTo = {ROCK}};
            ROCK.Render = new TileRenderComposite(new Sprite(Resources.TileTiles, 1)) {ConnectTo = {IRON_ORE}};
            SAND.Render = new TileRenderComposite(new Sprite(Resources.TileTiles, 3));
            VOID.Render = new TileRenderComposite(new Sprite(Resources.TileTiles, 8)) {ConnectTo = {WATER}};
            WATER.Render = new TileRenderComposite(new Sprite(Resources.TileTiles, 4)) {ConnectTo = {VOID}};
        }

        public static void AttachTags()
        {
            DIRT.AddTag(new DamageTile {ReplacementTile = VOID});

            GRASS.AddTag(new BreakableTile {ReplacementTile = DIRT});
            GRASS.AddTag(new DroppableTile(new Drop(ITEMS.GRASS_PATCH, 1f, 1, 1)));
            GRASS.AddTag(new Spread {SpreadChance = 50, SpreadTo = {DIRT}});

            IRON_ORE.AddTag(new DroppableTile(new Drop(ITEMS.IRON_ORE, 1f, 1, 2)));
            IRON_ORE.AddTag(new SolideTile(), new DamageTile {ReplacementTile = ROCK});
            IRON_ORE.BlockLineOfSight = true;

            ROCK.AddTag(new DroppableTile(new Drop(ITEMS.STONE, 1f, 2, 3), new Drop(ITEMS.COAL, 1f, 0, 2)));
            ROCK.AddTag(new SolideTile(), new DamageTile {ReplacementTile = DIRT});
            ROCK.BlockLineOfSight = true;

            SAND.AddTag(new BreakableTile {ReplacementTile = DIRT});
            SAND.AddTag(new DroppableTile(new Drop(ITEMS.SAND, 1f, 1, 1)));

            WATER.AddTag(new GroundTile {MoveSpeed = 0.5f});
            WATER.AddTag(new LiquideTile());
            WATER.AddTag(new Spread {SpreadChance = 1, SpreadTo = {VOID}});

            WOOD_FLOOR.AddTag(new DamageTile {ReplacementTile = DIRT});
            WOOD_FLOOR.AddTag(new DroppableTile(new Drop(ITEMS.WOOD_FLOOR, 1f, 1, 1)));

            WOOD_WALL.AddTag(new DamageTile {ReplacementTile = DIRT});
            WOOD_WALL.AddTag(new DroppableTile(new Drop(ITEMS.WOOD_WALL, 1f, 1, 1)));
            WOOD_WALL.AddTag(new SolideTile());
            WOOD_WALL.BlockLineOfSight = true;
        }

        public static Dictionary<string, string> GetIDToName()
        {
            var intToName = new Dictionary<string, string>();

            var id = 0;

            foreach (var t in GetTiles())
            {
                intToName.Add(id.ToString(), t.Name);

                id++;
            }

            return intToName;
        }

        public static Dictionary<Tile, int> GetTileToID()
        {
            var tileToInt = new Dictionary<Tile, int>();

            var id = 0;

            foreach (var t in GetTiles())
            {
                tileToInt.Add(t, id);

                id++;
            }

            return tileToInt;
        }
    }
}