using Hevadea.Worlds.Level;

namespace Hevadea.WorldGenerator.Functions
{
    public class CombinedFunction : IFunction
    {
        private readonly IFunction _functionA;
        private readonly IFunction _functionB;

        public CombinedFunction(IFunction functionA, IFunction functionB)
        {
            _functionA = functionA;
            _functionB = functionB;
        }

        public double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            return _functionA.Compute(x, y, gen, levelGen, level) * _functionB.Compute(x, y, gen, levelGen, level);
        }
    }
}