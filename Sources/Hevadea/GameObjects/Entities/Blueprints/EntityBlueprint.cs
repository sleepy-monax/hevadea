namespace Hevadea.GameObjects.Entities.Blueprints
{
    public abstract class EntityBlueprint
    {
        public string Name { get; }

        public EntityBlueprint(string name)
        {
            Name = name;
        }

        public abstract Entity Construct();
    }
}