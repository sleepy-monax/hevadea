using Hevadea.Registry;
using Hevadea.Tiles;
using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class TerrainLayer
    {
        public Tile Tile { get; set; } = TILES.WATER;
        public GeneratorFunctions Function { get; set; } = Functions.Flat(1f);
        public float Threashold { get; set; } = 1f;
        public int Priority { get; set; } = 0;
        public List<Tile> TileRequired { get; set; } = new List<Tile>();
    }

    public class Terrain : LevelFeature
    {
        public List<TerrainLayer> Layers { get; set; } = new List<TerrainLayer>();
        private float _progress = 0;

        public override string GetName()
        {
            return "Base Terrain";
        }

        public override float GetProgress()
        {
            return _progress;
        }

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            Layers.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            for (var x = 0; x < gen.Size; x++)
            {
                for (var y = 0; y < gen.Size; y++)
                {
                    var tile = TILES.VOID;

                    foreach (var layer in Layers)
                    {
                        var value = layer.Function(x, y, gen, levelGen, level);

                        var canBeAdded = value >= layer.Threashold &&
                                         (layer.TileRequired.Count == 0 || layer.TileRequired.Contains(tile));

                        if (canBeAdded) tile = layer.Tile;
                    }

                    level.SetTile(x, y, tile);
                }

                _progress = (float) x / gen.Size;
            }
        }
    }
}