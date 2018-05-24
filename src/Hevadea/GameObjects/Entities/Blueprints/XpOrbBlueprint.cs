namespace Hevadea.GameObjects.Entities.Blueprints
{
    public class XpOrbBlueprint : EntityBlueprint
    {
        private int _value;

        public XpOrbBlueprint(string name, int value) : base(name)
        {
            _value = value;
        }

        public override Entity Construct()
        {
            var e = new XpOrb(_value);
            e.Blueprint = this;
            return e;
        }
    }
}