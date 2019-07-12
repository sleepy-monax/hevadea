using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Utils;
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
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));
            _age = 0;
            _delay = 3f;

            AddComponent(new Breakable());
            AddComponent(new Collider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Explode(10f, 3f));
            AddComponent(new Move());
            AddComponent(new Pickupable());
            AddComponent(new Pushable());
            AddComponent(new CastShadow());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _age += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_age > _delay)
            {
                GetComponent<Explode>().Do();
                Remove();
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float factor = Easing.ExponentialEaseIn((_age / _delay));
            _sprite.Draw(spriteBatch, new Vector2((X - 8f - 8f * factor), (Y - 8f - 8f * factor)), 1f + factor, Color.White * (1.1f - factor));
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