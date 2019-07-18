using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    internal class TNT : Entity
    {
        private readonly Sprite _sprite;
        private float _age;
        private float _delay;

        public TNT()
        {
            _sprite = new Sprite(Resources.TileEntities, new Point(0, 1));
            _age = 0;
            _delay = 3f;

            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentCollider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new ComponentExplosive(false, 10f, 3f));
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPickupable());
            AddComponent(new Pushable());
            AddComponent(new ComponentCastShadow());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _age += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (_age > _delay)
            {
                GetComponent<ComponentExplosive>().Detonate();
                Remove();
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var factor = Easing.ExponentialEaseIn(_age / _delay);
            _sprite.Draw(spriteBatch, new Vector2(X - 8f - 8f * factor, Y - 8f - 8f * factor), 1f + factor,
                Color.White * (1.1f - factor));
        }

        public override void OnLoad(EntityStorage store)
        {
            _age = store.ValueOf("age", 0f);
        }

        public override void OnSave(EntityStorage store)
        {
            store.Value("age", _age);
        }
    }
}