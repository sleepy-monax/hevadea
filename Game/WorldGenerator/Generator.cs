using Hevadea.Framework;
using Hevadea.Framework.Threading;
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
        public int Size { get; set; } = 1024;

        public Noise Noise { get; private set; }
        public Random Random { get; private set; }

        public World Generate(Job job)
        {
            var w = new World();
            Random = new Random(Seed);
            Noise = new Noise(Seed);

            foreach (var levelGenerator in LevelsGenerators)
            {
                job.Report($"Generating {levelGenerator.Name}...");

                var level = new Level(levelGenerator.Properties, Size, Size)
                    {Id = levelGenerator.Id, Name = levelGenerator.Name};

                for (var i = 0; i < levelGenerator.Features.Count; i++)
                {
                    var generatorFeature = levelGenerator.Features[i];
                    job.Report(i / (float) levelGenerator.Features.Count);
                    generatorFeature.Apply(this, levelGenerator, level);
                }

                w.AddLevel(level);
            }

            foreach (var feature in WorldFeatures) feature.Apply(this, w);

            return w;
        }
    }
}