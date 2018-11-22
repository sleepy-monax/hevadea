using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Renderer;
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

            AddComponent(new Move());
            AddComponent(new Pickupable());
            AddComponent(new Pushable());
            AddComponent(new SpriteRenderer
            {
                Sprite = new Sprite(Ressources.TileEntities, new Point(4, 1)),
                Offset = new Vector2(0, -3f)
            });

            AddComponent(new Breakable());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.LANTERN, 1f, 1, 1) } });
            AddComponent(new LightSource { IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 96 });
            AddComponent(new Colider(new Rectangle(-4, -2, 7, 4)));

        }
    }
}