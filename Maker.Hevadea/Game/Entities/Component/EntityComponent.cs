namespace Maker.Hevadea.Game.Entities.Component
{
    public abstract class EntityComponent
    {
        public byte Priority = 0;
        public Entity Owner;
    }
}