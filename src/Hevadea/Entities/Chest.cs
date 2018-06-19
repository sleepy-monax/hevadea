using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hevadea.Registry;

namespace Hevadea.Entities
{
    public class Chest : Entity
    {
        private readonly Sprite _sprite;

        public Chest()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));

            AddComponent(new Burnable(1f));
            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Dropable() { Items = { new Items.Drop(ITEMS.CHEST, 1f, 1, 1) } });
            AddComponent(new Health(10));
            AddComponent(new Interactable()).Interacted += EntityInteracte;
            AddComponent(new Inventory(128));
            AddComponent(new Move());
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Pushable());
            AddComponent(new Shadow());
        }

        private void EntityInteracte(object sender, InteractEventArg args)
        {
            if (args.Entity.HasComponent<Inventory>())
                GameState.CurrentMenu = new MenuChest(args.Entity, this, GameState);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}