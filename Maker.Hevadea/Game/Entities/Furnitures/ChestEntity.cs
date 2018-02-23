using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Scenes.Menus;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Furnitures
{
    public class ChestEntity : Entity
    {
        private readonly Sprite _sprite;

        public ChestEntity()
        {
            Width = 12;
            Height = 9;
            Origin = new Point(8, 6);
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));

            Components.Add(new Inventory(128));
            Components.Add(new Health(10)).OnDie +=
                (sender, arg) =>
                {
                    Components.Get<Inventory>().Content.DropOnGround(Level, X + Origin.X, Y + Origin.Y);
                    ITEMS.CHEST.Drop(Level, X + Origin.X, Y + Origin.Y, 1);
                };

            Components.Add(new Interactable()).OnInteracte +=
                (sender, arg) =>
                {
                    if (arg.Entity.Components.Has<Inventory>())
                        Game.CurrentMenu = new ChestMenu(arg.Entity, this, Game);
                };
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 2, (int) Y - 5, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is PlayerEntity || e is ZombieEntity;
        }
    }
}