using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.Game.Registry
{
    public static class TILES
    {
        public static Tile[] ById = new Tile[256];

        public static VoidTile VOID;
        public static GrassTile GRASS;
        public static SandTile SAND;
        public static WaterTile WATER;
        public static RockTile ROCK;
        public static WoodFloorTile WOOD_FLOOR;
        public static WoodWallTile WOOD_WALL;
        public static DirtTile DIRT;

        public static void Initialize()
        {
            VOID       = new VoidTile(0);
            GRASS      = new GrassTile(1);
            SAND       = new SandTile(2);
            WATER      = new WaterTile(3);
            ROCK       = new RockTile(4);
            WOOD_FLOOR = new WoodFloorTile(5);
            WOOD_WALL  = new WoodWallTile(6);
            DIRT       = new DirtTile(7);
        }
    }
}
