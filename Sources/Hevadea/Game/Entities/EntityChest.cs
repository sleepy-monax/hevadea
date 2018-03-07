using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityChest : Entity
    {
        private readonly Sprite _sprite;

        public EntityChest()
        {
            Width = 12;
            Height = 9;
            Origin = new Point(8, 6);
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));

            Add(new Inventory(128));
            Add(new Health(10)).OnDie +=
                (sender, arg) =>
                {
                    Get<Inventory>().Content.DropOnGround(Level, X + Origin.X, Y + Origin.Y);
                    ITEMS.CHEST.Drop(Level, X + Origin.X, Y + Origin.Y, 1);
                };

            Add(new Interactable()).OnInteracte +=
                (sender, arg) =>
                {
                    if (arg.Entity.Has<Inventory>())
                        Game.CurrentMenu = new MenuItemContainer(arg.Entity, this, Game);
                };
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 2, (int) Y - 5, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is EntityPlayer || e is EntityZombie;
        }
    }
}