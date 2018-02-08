using System.Collections.Generic;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.WorldGenerator.Features.Functions;

namespace Maker.Hevadea.WorldGenerator.Features
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