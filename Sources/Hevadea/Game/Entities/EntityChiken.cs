using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
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
    class EntityChiken:Entity
    {
        private int dx = 0;
        private int dy = 0;
        private bool Move = false;
        private readonly Sprite _sprite;
        public EntityChiken()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(12, 0));
            Attach(new Move());
            Attach(new Health(3));
            Attach(new Colider(new Rectangle(-4, -4, 8, 8)));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Move = Rise.Random.Next(100) == 5 ? !Move:Move;  
            if (Move)
            {
                if (Rise.Random.Next(0, 20) == 0)
                {
                    dx = Rise.Random.Next(-20, 21);
                    dy = Rise.Random.Next(-20, 21);
                }
                var move = Get<Move>();

                if (dx != 0 || dy != 0)
                {
                    move.MoveTo(X + dx, Y + dy, null, 0.5f);
                }

            }
           
            
            
        }
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}
