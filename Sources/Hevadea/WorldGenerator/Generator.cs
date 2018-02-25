using Hevadea.Framework.Utils;
using Hevadea.Game;
using System;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator
{
    public class Generator
    {
        public List<LevelGenerator> Levels { get; set; } = new List<LevelGenerator>();
        public List<WorldFeature> WorldFeatures { get; set; } = new List<WorldFeature>();
        public int Seed { get; set; } = 0;
        public int Size { get; set; } = 256;

        public PerlinNoise Perlin { get; private set; }
        public Random Random { get; private set; }

        public LevelGenerator CurrentLevel = null;
        
        public World Generate()
        {
            var w = new World();
            Random = new Random(Seed);
            Perlin = new PerlinNoise(Seed);

            foreach (var l in Levels)
            {
                CurrentLevel = l;
                w.AddLevel(l.Generate(this));
            }

            foreach (var wf in WorldFeatures)
            {
                wf.Apply(this, w);
            }
            
            return w;
        }
    }
}