using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using System.Collections.Generic;

namespace Maker.Hevadea.WorldGenerator.Features
{
    public class BaseTerainFeature : GenFeature
    {
        public List<TerrainLayer> Layers { get; set; } = new List<TerrainLayer>();

        public override string GetName()
        {
            return "Base Terrain";
        }

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            Layers.Sort((a, b) => (a.Priority.CompareTo(b.Priority)));

            for (int x = 0; x < gen.Size; x++)
            {
                for (int y = 0; y < gen.Size; y++)
                {
                    Tile tile = TILES.VOID;

                    foreach (var layer in Layers)
                    {
                        var value = layer.Function.Compute((double)x, (double)y, gen, levelGen, level);

                        bool canBeAdded = (value >= layer.Threashold) && (layer.TileRequired.Count == 0 || layer.TileRequired.Contains(tile));

                        if (canBeAdded)
                        {
                            tile = layer.Tile;
                        }
                    }

                    level.SetTile(x, y, tile);
                }
            }
        }
    }
}
