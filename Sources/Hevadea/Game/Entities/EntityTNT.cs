using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Game.Entities
{
    class EntityTNT: Entity
    {
        private readonly Sprite _sprite;
        private float _age;
        private float _delay;
        
        public EntityTNT()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));
            _age = 0;
            _delay = 3f;
            Attach(new Move());
            Attach(new Colider(new Rectangle(-6, -2, 12, 8)));
            Attach(new Explode(10f,3f));
            Attach(new Pushable());
            Attach(new Breakable());
            Attach(new Pickupable(_sprite));
        }
        public override void OnUpdate(GameTime gameTime)
        {
            _age += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_age > _delay)
            {
                Get<Explode>().Do();
                Remove();
            }

        }
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float factor = Easing.ExponentialEaseIn((_age / _delay));
            _sprite.Draw(spriteBatch, new Vector2((X -8f - 8f * factor), (Y-8f - 8f * factor)), 1f + factor , Color.White * (1.1f - factor));
            
        }
        public override void OnLoad(EntityStorage store)
        {
            _age = store.GetFloat("age",0f);
        }
        public override void OnSave(EntityStorage store)
        {
            store.Set("age", _age);
        }
    }
}
