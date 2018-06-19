namespace Hevadea.Entities.Blueprints
{
    public abstract class EntityBlueprint
    {
        public string Name { get; }

        public abstract Entity Construct();

        public EntityBlueprint(string name)
        {
            Name = name;
        }
    }
}