using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityFurnace : Entity
    {
        private readonly Sprite _sprite;

        public EntityFurnace()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 1));
            Attachs(
                new Breakable(),
                new Light(),
                new Dropable {Items = { new Drop(ITEMS.FURNACE, 1f, 1, 1)}}
            );
            Attach(new Colider(new Rectangle(-8, 0, 16, 8)));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}