using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Lamp : Entity
    {
        public Lamp()
        {
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPickupable());
            AddComponent(new ComponentPushable());
            AddComponent(new RendererSprite(Resources.Sprites["entity/lamp"], new Vector2(0, -3f)));
            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentDropable {Items = {new Drop(ITEMS.LANTERN, 1f, 1, 1)}});
            AddComponent(new ComponentLightSource
                {IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 96});
            AddComponent(new ComponentCollider(new Rectangle(-4, -2, 7, 4)));
        }
    }
}