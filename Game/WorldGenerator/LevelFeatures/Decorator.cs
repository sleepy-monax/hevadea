using Hevadea.Entities.Blueprints;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Tiles;
using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class Decorator : LevelFeature
    {
        public List<Tile> CanBePlantOn { get; set; } = new List<Tile>();
        public GeneratorFunctions PlacingFunction { get; set; } = Functions.Flat(0.9f);
        public float Threashold { get; set; } = 1f;
        public int Chance { get; set; } = 1;
        public int RandomOffset { get; set; } = 4;
        public int Spacing { get; set; } = 1;

        private float _progress = 0;
        private EntityBlueprint _blueprint;

        public Decorator(EntityBlueprint blueprint)
        {
            _blueprint = blueprint;
        }

        public override float GetProgress()
        {
            return _progress;
        }

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            Logger.Log<Decorator>(GetName());
            for (var x = 0; x < gen.Size; x += Spacing)
            {
                for (var y = 0; y < gen.Size; y += Spacing)
                {
                    var coordinates = new Coordinates(x, y);
                    if (gen.Random.Next(0, Chance) == 0 &&
                        CanBePlantOn.Contains(level.GetTile(coordinates)) &&
                        PlacingFunction(x, y, gen, levelGen, level) < Threashold &&
                        !level.AnyEntityAt(coordinates))
                        level.AddEntityAt(_blueprint, coordinates, gen.Random.NextVector2(-RandomOffset, RandomOffset));
                }

                _progress = x / (float) gen.Size;
            }
        }

        public override string GetName()
        {
            return $"Populating [{_blueprint.Name}]";
        }
    }
}