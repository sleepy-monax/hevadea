using Hevadea.Framework.Graphic.SpriteAtlas;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Pickupable : Component
    {
        public Sprite OnPickupSprite { get; set; }

        public Pickupable(Sprite pickedupSprite)
        {
            OnPickupSprite = pickedupSprite;
        }
    }
}