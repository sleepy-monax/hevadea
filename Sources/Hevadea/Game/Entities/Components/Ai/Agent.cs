using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Ai
{


    
    
    public class Agent: EntityComponent, IEntityComponentUpdatable
    {
        public void Update(GameTime gameTime)
        {
           
        }

        public void MoveTo(float x, float y,  Direction? direction = null, float speed = 1f)
        {
            var dir = new Vector2(x - Owner.X, y - Owner.Y);
            dir.Normalize();
            dir = dir * speed;

            Owner.Get<Move>()?.Do(dir.X, dir.Y, direction ?? dir.ToDirection());
        }
    }
}