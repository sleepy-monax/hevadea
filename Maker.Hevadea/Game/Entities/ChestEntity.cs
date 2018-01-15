using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Menus;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class ChestEntity : Entity
    {

        private Sprite closeSprite;

        public ChestEntity()
        {
            Width       = 12;
            Height      = 9;
            Origin      = new Point(8, 8);
            closeSprite = new Sprite(Ressources.tile_entities, new Point(1,1));

            Components.Add(new InventoryComponent(512));
            Components.Add(new InteractableComponent());
            Components.Get<InteractableComponent>().OnInteracte += (sender, arg) =>
            {
                if (arg.Entity.Components.Has<InventoryComponent>())
                {
                    Game.SetMenu(new ChestMenu(arg.Entity, this, World, Game));
                }
            };
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            closeSprite.Draw(spriteBatch, new Rectangle((int)X - 2, (int)Y - 5, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is PlayerEntity || e is ZombieEntity;
        }
    }
}