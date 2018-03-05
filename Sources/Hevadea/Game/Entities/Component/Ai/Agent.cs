using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hevadea.Game.Entities.Component.Attributes;

namespace Hevadea.Game.Entities.Component.Ai
{
    public class Agent: EntityComponent, IUpdatableComponent
    {
        public void Update(GameTime gameTime)
        {
           
        }

        public void MoveTo(float x, float y, float speed = 1f)
        {
            var moveComponent = Owner.Get<Move>();

            if (moveComponent != null)
            {
                var dir = new Vector2(x - Owner.X, y - Owner.Y);
                dir.Normalize();
                dir = dir * speed;

                moveComponent.Do(dir.X, dir.Y, dir.ToDirection());
            }
        }

        public void MoveTo(float x, float y,  Direction direction, float speed = 1f)
        {
            var moveComponent = Owner.Get<Move>();
            
            if (moveComponent != null)
            {
                var dir = new Vector2(x - Owner.X, y - Owner.Y);
                dir.Normalize();
                dir = dir * speed;

                moveComponent.Do(dir.X, dir.Y, direction);
            }
        }
    }
}