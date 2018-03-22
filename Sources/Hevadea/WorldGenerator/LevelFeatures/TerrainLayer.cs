using Hevadea.Game.Registry;
using Hevadea.WorldGenerator.Functions;
using System.Collections.Generic;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class TerrainLayer
    {
        public Tile Tile { get; set; } = TILES.WATER;
        public IFunction Function { get; set; } = new FlatFunction(1f);
        public float Threashold { get; set; } = 1f;
        public int Priority { get; set; } = 0;
        public List<Tile> TileRequired { get; set; } = new List<Tile>();
    }
}