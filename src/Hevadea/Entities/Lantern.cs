using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Lantern : Entity
    {
        private readonly Sprite _sprite;

        public Lantern()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(4, 1));

            AddComponent(new Pickupable(_sprite));
            AddComponent(new Breakable());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.TORCH, 1f, 1, 1) } });
            AddComponent(new LightSource { IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 96 });
            AddComponent(new Colider(new Rectangle(-4, -2, 7, 4)));

            AddComponent(new Pushable());
            AddComponent(new Physic());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 8, Y - 11), Color.White);
        }
    }
}