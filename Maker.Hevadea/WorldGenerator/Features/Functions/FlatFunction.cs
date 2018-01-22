using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Hevadea.Game;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public class FlatFunction : FunctionBase
    {
        private double Value;

        public FlatFunction(double value)
        {
            Value = value;
        }

        public override double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            return Value;
        }
    }
}
