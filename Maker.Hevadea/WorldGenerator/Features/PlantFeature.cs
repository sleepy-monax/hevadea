using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.WorldGenerator.Features.Functions;
using System.Collections.Generic;
using System.Linq;

namespace Maker.Hevadea.WorldGenerator.Features
{
    public class PlantFeature<T> : GenFeature where T : Entity, new()
    {
        public List<Tile> CanBePlantOn { get; set; } = new List<Tile>();
        public FunctionBase PlacingFunction { get; set; } = new FlatFunction(0.9f);
        public float Threashold { get; set; } = 1f;
        public int Chance { get; set; } = 1;
        public int RandomOffset { get; set; } = 4;

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            for (int x = 0; x < gen.Size; x++)
            {
                for (int y = 0; y < gen.Size; y++)
                {
                    if ((gen.Random.Next(0,Chance) == 0) &&
                        (CanBePlantOn.Contains(level.GetTile(x, y)))&&
                        (PlacingFunction.Compute(x, y, gen, levelGen, level) < Threashold) &&
                        (!level.GetEntityOnTile(x,y).Any()))
                    {
                        level.SpawnEntity(new T(), x, y, gen.Random.Next(-RandomOffset, RandomOffset), gen.Random.Next(-RandomOffset, RandomOffset));
                    }
                }
            }
        }

        public override string GetName()
        {
            return $"Planting [{new T().GetType().Name}]";
        }
    }
}
