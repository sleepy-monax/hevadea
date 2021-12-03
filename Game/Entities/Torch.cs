using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Torch : Entity
    {
        public Torch()
        {
            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentDropable {Items = {new Drop(ITEMS.TORCH, 1f, 1, 1)}});
            AddComponent(new ComponentLightSource
                {IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72});
            AddComponent(new RendererSprite(Resources.Sprites["entity/item"], new Vector2(4, 0)));
        }
    }
}