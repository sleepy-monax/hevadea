using Maker.Hevadea.Game;

namespace Maker.Hevadea.WorldGenerator
{
    public abstract class GenFeature
    {
        public abstract string GetName();
        public abstract void Apply(Generator gen, LevelGenerator levelGen, Level level);
    }
}
