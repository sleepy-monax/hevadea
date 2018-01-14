using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Items
{
    public class RessourceItem : Item
    {
        private readonly Sprite Sprite;

        public RessourceItem(byte id, string name, Sprite sprite) : base(id, name)
        {
            Sprite = sprite;
        }

        public override Sprite GetSprite()
        {
            return Sprite;
        }
    }
}
