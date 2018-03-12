using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
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
        private int dx = 0;
        private int dy = 0;
        
        private readonly Sprite _sprite;
        
        public EntityFish()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(11, 0));
            Attach(new Move());
            Attach(new Swim() { IsSwimingPainfull = false});
            Attach(new Breakable());
            Attach(new Colider(new Rectangle(-4, -4, 8, 8)));
            Attach(new Dropable { Items = { new Drop(ITEMS.RAW_FISH, 1f, 1, 1) } });
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Rise.Random.Next(0,20) == 0)
            {
                dx = Rise.Random.Next(-20, 21);
                dy = Rise.Random.Next(-20, 21);
            }
            var move = Get<Move>();
            if (dx != 0 || dy !=0)
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
