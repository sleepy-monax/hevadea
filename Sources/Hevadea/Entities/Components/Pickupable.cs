using Hevadea.Framework.Graphic.SpriteAtlas;

namespace Hevadea.Entities.Components
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