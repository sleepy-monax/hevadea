using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Bench : Entity
    {
        public Bench()
        {
            AddComponent(new ComponentMove());
            AddComponent(new RendererSprite(Resources.Sprites["entity/bench"]));
            AddComponent(new ComponentCraftingStation(RECIPIES.BenchCrafted));
            AddComponent(new ComponentCastShadow());
            AddComponent(new ComponentPickupable());
            AddComponent(new ComponentFlammable());

            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentCollider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new ComponentDropable {Items = {new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1)}});
            AddComponent(new ComponentPushable());
        }
    }
}