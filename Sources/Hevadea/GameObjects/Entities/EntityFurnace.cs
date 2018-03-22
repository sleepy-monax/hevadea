using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Registry;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Hevadea.GameObjects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public class EntityFurnace : Entity
    {
        private readonly Sprite _sprite;

        public EntityFurnace()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 1));
            
            Attach(new Breakable());
            Attach(new Light());
            Attach(new Dropable {Items = { new Drop(ITEMS.FURNACE, 1f, 1, 1)}});
            Attach(new Move());
            Attach(new Pushable {CanBePushByAnything = true});
            Attach(new Colider( new Rectangle(-6, -2, 12, 8) ));
            Attach(new Pickupable(_sprite));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}