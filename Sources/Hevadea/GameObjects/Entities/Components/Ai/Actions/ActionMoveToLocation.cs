using Hevadea.Framework.Graphic;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Ai.Actions
{
    public class ActionMoveToLocation : IAction
    {
        private readonly TilePosition _destination;
        private readonly float        _speed;
        
        public ActionMoveToLocation(TilePosition destination, float speed = 1f)
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
            agent.Owner.GetComponent<Move>()?.MoveTo(_destination, null, _speed);
            
            if (agentPosition == agent.Owner.Position)
            {
                // Agent Stuck... abort
                agent.Abort(AgentAbortReason.ImStuck);
            }
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(_destination.WorldX, _destination.WorldY, 16, 16, Color.AliceBlue * 0.75f);
        }
    }
}