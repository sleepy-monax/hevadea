using Hevadea.Entities.Components;
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
            AddComponent(new ComponentPickupable());
            AddComponent(new RendererSprite {Sprite = new Sprite(Resources.TileEntities, new Point(0, 1))});
            AddComponent(new ComponentFlammable());

            AddComponent(new ComponentCollider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new ComponentDropable() {Items = {new Items.Drop(ITEMS.CHEST, 1f, 1, 1)}});
            AddComponent(new ComponentHealth(10));
            AddComponent(new ComponentInteractable()).Interacted += EntityInteracte;
            AddComponent(new Inventory(128));
            AddComponent(new ComponentMove());
            AddComponent(new Pushable());
            AddComponent(new ComponentCastShadow());
        }

        private void EntityInteracte(object sender, InteractEventArg args)
        {
            if (args.Entity.HasComponent<Inventory>())
                GameState.CurrentMenu = new MenuChest(args.Entity, this, GameState);
        }
    }
}