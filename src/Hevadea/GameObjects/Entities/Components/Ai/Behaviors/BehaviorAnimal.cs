using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Ai.Actions;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities.Components.Ai.Behaviors
{
    public class BehaviorAnimal : IBehavior
    {
        public List<Tile> NaturalEnvironment { get; set; } = new List<Tile>();
        public float MoveSpeedWandering { get; set; } = 1f;
        public float IdleChance { get; set; } = 0.5f;
        public float IdleTime { get; set; } = 1.0f;

        public virtual void Update(Agent agent, GameTime gameTime)
        {
            if (agent.IsBusy()) return;

            if (Rise.Rnd.NextFloat() < IdleChance)
            {
                agent.ActionQueue.Enqueue(new ActionWait(Rise.Rnd.NextFloatRange(IdleTime)));
            }
            else
            {
                var dx = Rise.Rnd.NextValue(-1, 0, 1);
                var dy = Rise.Rnd.NextValue(-1, 0, 1);

                if (dx == 0)
                {
                    Rise.Rnd.NextValue(-1, 1);
                }

                var entityTilePosition = agent.Owner.GetTilePosition();
                var destination = new TilePosition(entityTilePosition.X + dx, entityTilePosition.Y + dy);

                if ((dx != 0 || dy != 0) && (NaturalEnvironment.Contains(agent.Owner.Level.GetTile(destination)) || NaturalEnvironment.Count == 0))
                {
                    agent.MoveTo(destination, MoveSpeedWandering);
                }
            }
        }

        public virtual void IaAborted(Agent agent, AgentAbortReason why)
        {
        }

        public void IaFinish(Agent agent)
        {
        }

        public virtual void DrawDebug(SpriteBatch spriteBatch, Agent agent, GameTime gameTime)
        {

        }
    }
}