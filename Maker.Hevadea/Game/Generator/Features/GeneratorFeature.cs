namespace Maker.Hevadea.Game.Generator
{
    public abstract class GeneratorFeature
    {
        public readonly string Name;

        public GeneratorFeature(string name)
        {
        }

        public void Apply(Level level, GeneratorBase generator)
        {
            ApplyInternal(level, generator);
        }

        public abstract void ApplyInternal(Level level, GeneratorBase generator);
    }
}