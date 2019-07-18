using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Lantern : Entity
    {
        public Lantern()
        {
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPickupable());
            AddComponent(new Pushable());
            AddComponent(new RendererSprite
            {
                Sprite = new Sprite(Resources.TileEntities, new Point(4, 1)),
                Offset = new Vector2(0, -3f)
            });

            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentDropable {Items = {new Drop(ITEMS.LANTERN, 1f, 1, 1)}});
            AddComponent(new ComponentLightSource
                {IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 96});
            AddComponent(new ComponentCollider(new Rectangle(-4, -2, 7, 4)));
        }
    }
}