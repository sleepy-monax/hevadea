using Hevadea.Game;
using Hevadea.Game.Worlds;

namespace Hevadea.WorldGenerator.Functions
{
    public class FlatFunction : IFunction
    {
        private readonly double _value;

        public FlatFunction(double value)
        {
            _value = value;
        }

        public double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            return _value;
        }
    }
}