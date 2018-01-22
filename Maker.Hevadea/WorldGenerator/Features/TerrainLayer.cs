using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.WorldGenerator.Features.Functions;
using System.Collections.Generic;

namespace Maker.Hevadea.WorldGenerator.Features
{
    public class TerrainLayer
    {
        public Tile Tile { get; set; } = TILES.WATER;
        public FunctionBase Function { get; set; } = new FlatFunction(1f);
        public float Depth { get;  set; } = 1f;
        public int Priority { get; set; } = 0;
        public List<Tile> TileRequired { get; set; } = new List<Tile>();
    }
}
