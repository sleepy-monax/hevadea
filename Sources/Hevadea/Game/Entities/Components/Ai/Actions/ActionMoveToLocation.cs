using Hevadea.Framework.Graphic;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Ai.Actions
{
    public class ActionMoveToLocation : IAction
    {
        private TilePosition _targetPosition;
        private float _speed;
        
        public ActionMoveToLocation(TilePosition targetPosition, float speed = 1f)
        {
            _targetPosition = targetPosition;
            _speed = speed;
        }
        
        public bool IsStillRunning(Agent agent)
        {
            return !(_targetPosition.GetCenter() == agent.Owner.Position) && agent.Owner.Has<Move>();
        }

        public void Perform(Agent agent, GameTime gameTime)
        {
            var agentPosition = agent.Owner.Position;
            agent.Owner.Get<Move>()?.MoveTo(_targetPosition, null, _speed);

            if (agentPosition == agent.Owner.Position)
            {
                // Agent Stuck... abort
                agent.Abort(AgentAbortReason.ImStuck);
            }
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(_targetPosition.WorldX, _targetPosition.WorldY, 16, 16, Color.AliceBlue * 0.75f);
        }
    }
}