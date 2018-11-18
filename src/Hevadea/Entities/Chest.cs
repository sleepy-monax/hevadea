using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Chest : Entity
    {
        public Chest()
        {
            AddComponent(new Pickupable());
            AddComponent(new SpriteRenderer { Sprite = new Sprite(Ressources.TileEntities, new Point(0, 1)) });
            AddComponent(new Flammable());

            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Dropable() { Items = { new Items.Drop(ITEMS.CHEST, 1f, 1, 1) } });
            AddComponent(new Health(10));
            AddComponent(new Interactable()).Interacted += EntityInteracte;
            AddComponent(new Inventory(128));
            AddComponent(new Move());
            AddComponent(new Pushable());
            AddComponent(new ShadowCaster());
        }

        private void EntityInteracte(object sender, InteractEventArg args)
        {
            if (args.Entity.HasComponent<Inventory>())
                GameState.CurrentMenu = new MenuChest(args.Entity, this, GameState);
        }
    }
}