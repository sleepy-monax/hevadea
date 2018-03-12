namespace Hevadea.Game.Entities.Components
{
    public abstract class EntityComponent
    {
        public Entity AttachedEntity;
        public byte Priority { get; set; } = 0;
    }
}