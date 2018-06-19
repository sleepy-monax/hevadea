using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hevadea.Registry;

namespace Hevadea.Entities
{
    public class Furnace : Entity
    {
        private readonly Sprite _sprite;

        public Furnace()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 1));

            AddComponent(new Breakable());
            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.FURNACE, 1f, 1, 1) } });
            AddComponent(new LightSource());
            AddComponent(new Move());
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Pushable());
            AddComponent(new Shadow());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}