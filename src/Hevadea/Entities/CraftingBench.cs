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
    public class CraftingBench : Entity
    {
        public CraftingBench()
        {
            AddComponent(new Physic());
            AddComponent(new SpriteRenderer(new Sprite(Ressources.TileEntities, new Point(1, 0))));
            AddComponent(new CraftingStation(RECIPIES.BenchCrafted));
            AddComponent(new ShadowCaster());
            AddComponent(new Pickupable());

            AddComponent(new Breakable());
            AddComponent(new Burnable(1f));
            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1) } });
            AddComponent(new Pushable());
        }
    }
}