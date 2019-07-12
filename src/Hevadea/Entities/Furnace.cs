using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic.SpriteAtlas;
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
            AddComponent(new Pickupable());
            AddComponent(new SpriteRenderer { Sprite = new Sprite(Ressources.TileEntities, new Point(1, 1)) });

            AddComponent(new Breakable());
            AddComponent(new Collider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.FURNACE, 1f, 1, 1) } });
            AddComponent(new LightSource());
            AddComponent(new Move());
            AddComponent(new Pushable());
            AddComponent(new ShadowCaster());
        }
    }
}