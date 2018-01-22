using Maker.Hevadea.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public abstract class FunctionBase
    {
        public abstract double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level);
    }
}
