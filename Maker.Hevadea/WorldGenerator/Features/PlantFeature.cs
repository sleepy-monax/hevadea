using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.WorldGenerator.Features.Functions;

namespace Maker.Hevadea.WorldGenerator.Features
{
    public class PlantFeature<T> : GenFeature where T : Entity, new()
    {
        public List<Tile> CanBePlantOn { get; set; } = new List<Tile>();
        public IFunction PlacingFunction { get; set; } = new FlatFunction(0.9f);
        public float Threashold { get; set; } = 1f;
        public int Chance { get; set; } = 1;
        public int RandomOffset { get; set; } = 4;
        
        public float _progress = 0;
        
        public override float GetProgress()
        {
            return _progress;
        }

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            for (var x = 0; x < gen.Size; x++)
            {
                for (var y = 0; y < gen.Size; y++)
                {   
                    if (gen.Random.Next(0, Chance) == 0 && CanBePlantOn.Contains(level.GetTile(x, y)) && PlacingFunction.Compute(x, y, gen, levelGen, level) < Threashold && level.GetEntityOnTile(x, y).Count == 0)
                        level.SpawnEntity(new T(), x, y, gen.Random.Next(-RandomOffset, RandomOffset), gen.Random.Next(-RandomOffset, RandomOffset));
                }
                
                _progress = (x / (float) gen.Size);
            }                                                              
        }

        public override string GetName()
        {
            return $"Planting [{new T().GetType().Name}]";
        }
    }
}