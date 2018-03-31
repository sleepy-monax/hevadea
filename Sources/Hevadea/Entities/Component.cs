namespace Hevadea.Entities
{
    public abstract class Component
    {
        public Entity Owner;
        public byte Priority { get; set; } = 0;
    }
}