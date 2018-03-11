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
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));
            
            Attachs(                
                new Pushable(){ CanBePushByAnything = true },
                new Move(),
                new Colider(new Rectangle(-6, -2, 12, 8)));
            
            Attach(new Inventory(128));
            Attach(new Health(10)).OnDie +=
                (sender, arg) =>
                {
                    Get<Inventory>().Content.DropOnGround(Level, X, Y);
                    ITEMS.CHEST.Drop(Level, X, Y, 1);
                };

            Attach(new Interactable()).OnInteracte +=
                (sender, arg) =>
                {
                    if (arg.Entity.Has<Inventory>())
                        Game.CurrentMenu = new MenuItemContainer(arg.Entity, this, Game);
                };

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}