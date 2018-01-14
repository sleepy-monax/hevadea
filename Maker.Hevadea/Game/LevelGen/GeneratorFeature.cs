namespace Maker.Hevadea.Game.LevelGen
{
    public abstract class GeneratorFeature
    {
        public readonly string Name;

        public GeneratorFeature(string name)
        {
        }

        public void Apply(Level level, Generator generator)
        {
            ApplyInternal(level, generator);
        }

        public abstract void ApplyInternal(Level level, Generator generator);
    }
}