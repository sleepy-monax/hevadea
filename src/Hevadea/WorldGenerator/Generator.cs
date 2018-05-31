using Hevadea.Framework.Threading;
using Hevadea.Framework.Utils;
using Hevadea.Worlds;
using System;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator
{
    public class Generator
    {
        public List<LevelGenerator> LevelsGenerators { get; set; } = new List<LevelGenerator>();
        public List<WorldFeature> WorldFeatures { get; set; } = new List<WorldFeature>();
        public int Seed { get; set; } = 0;
        public int Size { get; set; } = 256;

        public PerlinNoise Perlin { get; private set; }
        public Random Random { get; private set; }

        public World Generate(Job job)
        {
            var w = new World();
            Random = new Random(Seed);
            Perlin = new PerlinNoise(Seed);

            foreach (var levelGenerator in LevelsGenerators)
            {
                job.Report($"Generating {levelGenerator.Name}...");

                Level level = new Level(levelGenerator.Properties, Size, Size) { Id = levelGenerator.Id, Name = levelGenerator.Name };

                for (int i = 0; i < levelGenerator.Features.Count; i++)
                {
                    var f = levelGenerator.Features[i];
                    job.Report(i / (float)levelGenerator.Features.Count);
                    f.Apply(this, levelGenerator, level);
                }

                w.AddLevel(level);
            }

            foreach (var feature in WorldFeatures)
            {
                feature.Apply(this, w);
            }

            return w;
        }
    }
}