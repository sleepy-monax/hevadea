namespace Hevadea.Game.Entities.Component
{
    public abstract class EntityComponent
    {
        public Entity Owner;
        public byte Priority { get; set; } = 0;
    }
}