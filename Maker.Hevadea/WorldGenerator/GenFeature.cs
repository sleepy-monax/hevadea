using Maker.Hevadea.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.WorldGenerator
{
    public abstract class GenFeature
    {
        public abstract string GetName();
        public abstract void Apply(Generator gen, LevelGenerator levelGen, Level level);
    }
}
