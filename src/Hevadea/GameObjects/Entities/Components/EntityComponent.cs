namespace Hevadea.GameObjects.Entities.Components
{
    public abstract class EntityComponent
    {
        public Entity Owner;
        public byte Priority { get; set; } = 0;
    }
}