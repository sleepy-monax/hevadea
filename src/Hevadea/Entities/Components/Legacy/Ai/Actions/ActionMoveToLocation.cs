using Hevadea.Entities.Components.Actions;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.Ai.Actions
{
    public class ActionMoveToLocation : IAction
    {
        private readonly Coordinates _destination;
        private readonly float _speed;

        public ActionMoveToLocation(Coordinates destination, float speed = 1f)
        {
            _destination = destination;
            _speed = speed;
        }

        public bool IsStillRunning(Agent agent)
        {
            return !(_destination.GetCenter() == agent.Owner.Position) && agent.Owner.HasComponent<Move>();
        }

        public void Perform(Agent agent, GameTime gameTime)
        {
            var agentPosition = agent.Owner.Position;
            agent.Owner.GetComponent<Move>()?.MoveTo(_destination, _speed, true);

            if (agentPosition == agent.Owner.Position)
            {
                // Agent Stuck... abort
                agent.Abort(AgentAbortReason.ImStuck);
            }
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(_destination.WorldX + 4, _destination.WorldY + 4, 8, 8, Color.Blue * 0.75f);
        }
    }
}