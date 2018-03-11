using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Entities
{
    public class EntityFish:Entity
    {
        private readonly Sprite _sprite;
        public EntityFish()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));
            Attach(new Move());
            Attach(new Swim());
            Attach(new Breakable());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var dx = Rise.Random.Next(-1, 1);
            var dy = Rise.Random.Next(-1, 1);
            var move = Get<Move>();
            if (true)
            {
               move.MoveTo(X+dx, Y+dy);
            }
            
        }
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}
