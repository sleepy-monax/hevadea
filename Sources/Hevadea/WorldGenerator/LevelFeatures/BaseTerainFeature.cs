using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class BaseTerainFeature : LevelFeature
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
                    Tile tile = TILES.VOID;

                    foreach (var layer in Layers)
                    {
                        var value = layer.Function.Compute(x, y, gen, levelGen, level);

                        var canBeAdded = value >= layer.Threashold &&
                                         (layer.TileRequired.Count == 0 || layer.TileRequired.Contains(tile));

                        if (canBeAdded) tile = layer.Tile;
                    }

                    level.SetTile(x, y, tile);
                }

                _progress = (x / (float) gen.Size);
            }
        }
    }
}