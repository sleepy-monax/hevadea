using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Items
{
    public class RessourceItem : Item
    {
        private readonly Sprite Sprite;
        private readonly string Name;

        public RessourceItem(byte id, string name, Sprite sprite) : base(id)
        {
            Sprite = sprite;
            Name = name;
        }

        public override string GetName()
        {
            return Name;
        }

        public override Sprite GetSprite()
        {
            return Sprite;
        }
    }
}
