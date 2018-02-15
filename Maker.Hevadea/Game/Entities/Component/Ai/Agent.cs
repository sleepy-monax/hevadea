using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Ai
{
    public enum GenericAgentStates
    {
        Idle,
    }
    public class Agent<TState> : EntityComponent, IUpdatableComponent
    {
        public Entity Taget { get; set; }
        public Dictionary<TState, StateBahevior> States { get; set; } = new Dictionary<TState, StateBahevior>();
        public List<Trigger> Triggers { get; set; } = new List<Trigger>();
        public TState CurrentState { get; set; } = default(TState);

        public void Update(GameTime gameTime)
        {
            MoveTo(Owner.Game.Player.X, Owner.Game.Player.Y);
        }

        public void MoveTo(float x, float y, float speed = 1f)
        {
            var moveComponent = Owner.Components.Get<Move>();
            
            if (moveComponent != null)
            {
                var dir = new Vector2(x - Owner.X, y - Owner.Y);
                dir.Normalize();
                dir = dir * speed;

                moveComponent.Do(dir.X, dir.Y, Direction.Down);
            }
        }
    }
}