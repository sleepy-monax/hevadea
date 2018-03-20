using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Entities
{
    class EntityTNT: Entity
    {
        private readonly Sprite _sprite;
        private double _age;
        private double _delay;
        
        public EntityTNT()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));
            _age = 0;
            _delay = 10.00;
            Attach(new Explode(10f,3f));
            Attach(new Pushable());
            Attach(new Breakable());
            Attach(new Pickupable(_sprite));
        }
        public override void OnUpdate(GameTime gameTime)
        {
            _age += gameTime.ElapsedGameTime.TotalSeconds;
            if (_age > _delay)
            {
                Get<Explode>().Do();
                Remove();
            }

        }
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
        public override void OnLoad(EntityStorage store)
        {
            _age = store.GetFloat("age",0);
        }
        public override void OnSave(EntityStorage store)
        {
            store.Set("age", _age);
        }
    }
}
