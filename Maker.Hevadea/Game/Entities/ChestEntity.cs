using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.Registry;
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
            Origin      = new Point(8, 6);
            closeSprite = new Sprite(Ressources.tile_entities, new Point(1, 1));

            Components.Add(new Inventory(512));
            Components.Add(new Health(10)).OnDie +=
                (sender, arg) => 
                {
                    Components.Get<Inventory>().Content.DropOnGround(Level, X + Origin.X, Y + Origin.Y);
                    ITEMS.CHEST_ITEM.Drop(Level, X + Origin.X, Y + Origin.Y, 1);
                };

            Components.Add(new Interactable()).OnInteracte += 
                (sender, arg) =>
                {
                    if (arg.Entity.Components.Has<Inventory>())
                    {
                        Game.CurrentMenu = new ChestMenu(arg.Entity, this, Game);
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