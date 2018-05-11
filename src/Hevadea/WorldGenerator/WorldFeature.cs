using Hevadea.Worlds;

namespace Hevadea.WorldGenerator
{
    public abstract class WorldFeature
    {
        public abstract string GetName();

        public abstract float GetProgress();

        public abstract void Apply(Generator gen, World world);
    }
}