using System.Collections.Generic;
using Hevadea.Entities.Components.AI.Actions;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components.AI.Behaviors
{
    public class BehaviorAnimal : Behavior
    {
        public List<Tile> NaturalEnvironment { get; set; } = new List<Tile>();
        public float MoveSpeedWandering { get; set; } = 1f;
        public float IdleChance { get; set; } = 0.5f;
        public float IdleTime { get; set; } = 1.0f;

        public override void Update(GameTime gameTime)
        {
            if (Agent.IsBusy()) return;

            if (Rise.Rnd.NextFloat() < IdleChance)
            {
                Agent.ActionQueue.Enqueue(new ActionWait(Rise.Rnd.NextFloatRange(IdleTime)));
            }
            else
            {
                var dx = Rise.Rnd.Pick(-1, 1);
                var dy = Rise.Rnd.Pick(-1, 0, 1);

                var entityTilePosition = Agent.Owner.Coordinates;
                var destination = new Coordinates(entityTilePosition.X + dx, entityTilePosition.Y + dy);

                if ((dx != 0 || dy != 0) && (NaturalEnvironment.Contains(Agent.Owner.Level.GetTile(destination)) ||
                                             NaturalEnvironment.Count == 0))
                    Agent.MoveTo(destination, MoveSpeedWandering);
            }
        }

        public virtual void IaAborted(Agent agent, AgentAbortReason why)
        {
        }

        public void IaFinish(Agent agent)
        {
        }
    }
}