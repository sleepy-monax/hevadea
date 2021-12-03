using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    internal class TNT : Entity
    {
        private float _age;
        private float _delay;
        private _Sprite _sprite;

        public TNT()
        {
            _age = 0;
            _delay = 3f;

            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentCollider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new ComponentExplosive(false, 10f, 3f));
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPickupable());
            AddComponent(new ComponentPushable());
            AddComponent(new ComponentCastShadow());

            _sprite = Resources.Sprites["entity/tnt"];
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

            spriteBatch.DrawSprite(_sprite, Position - new Vector2(8f + 8f * factor), 1f + factor, Color.White);
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