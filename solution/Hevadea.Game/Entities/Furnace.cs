using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Furnace : Entity
    {
        public Furnace()
        {
            AddComponent(new ComponentPickupable());
            AddComponent(new RendererSprite(Resources.Sprites["entity/furnace"]));
            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentCollider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new ComponentDropable {Items = {new Drop(ITEMS.FURNACE, 1f, 1, 1)}});
            AddComponent(new ComponentLightSource());
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPushable());
            AddComponent(new ComponentCastShadow());
        }
    }
}