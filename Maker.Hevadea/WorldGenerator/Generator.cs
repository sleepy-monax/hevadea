using Maker.Hevadea.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.WorldGenerator
{
    public class Generator
    {
        public int Seed { get; set; } = 0;
        public int Size { get; set; } = 256;
        public Random Random { get; private set; }
        private List<LevelGenerator> levels;
        public List<LevelGenerator> Levels { get { return levels ?? (levels = new List<LevelGenerator>()); } }

        public World Generate()
        {
            var w = new World();
            Random = new Random(Seed);

            foreach (var l in levels)
            {
                w.AddLevel(l.Generate(this));
            }

            return w;
        }

    }
}
