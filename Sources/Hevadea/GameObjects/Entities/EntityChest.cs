using System;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Registry;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public class EntityChest : Entity
    {
        private readonly Sprite _sprite;

        public EntityChest()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));

            Attach(new Move());
            Attach(new Inventory(128));
            Attach(new Pickupable(_sprite));
            Attach(new Colider(new Rectangle(-6, -2, 12, 8)));
            Attach(new Pushable() {CanBePushByAnything = true});
            Attach(new Health(10)).Killed += EntityDie;
            Attach(new Interactable()).Interacted += EntityInteracte;
            Attach(new Pickupable(_sprite));
            Attach(new Burnable(1f));
        }

        private void EntityInteracte(object sender, InteractEventArg args)
        {
            if (args.Entity.HasComponent<Inventory>())
                Game.CurrentMenu = new MenuItemContainer(args.Entity, this, Game);
        }

        private void EntityDie(object sender, EventArgs args)
        {
            GetComponent<Inventory>().Content.DropOnGround(Level, X, Y);
            ITEMS.CHEST.Drop(Level, X, Y, 1);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}