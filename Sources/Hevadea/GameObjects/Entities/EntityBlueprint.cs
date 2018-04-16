namespace Hevadea.GameObjects.Entities
{
    public abstract class EntityBlueprint
    {
        public string Name { get; }

        public EntityBlueprint(string name)
        {
            Name = name;
        }

        public virtual Entity Construct()
        {
            var entity = new Entity();
            entity.Blueprint = this;

            AttachComponents(entity);

            return entity;
        }

        public abstract void AttachComponents(Entity e);
    }
}