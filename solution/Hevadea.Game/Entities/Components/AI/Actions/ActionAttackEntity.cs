using Hevadea.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.AI.Actions
{
    public class ActionAttackEntity : IAction
    {
        private readonly Entity _taget;
        private readonly float _range;

        public ActionAttackEntity(Entity e, float range)
        {
            _taget = e;
            _range = range;
        }

        public bool IsStillRunning(Agent agent)
        {
            // Check if the target is still in this level and in range.
            return !_taget.Removed && _taget.Level == agent.Owner.Level &&
                   Mathf.Distance(_taget.X, _taget.Y, agent.Owner.X, agent.Owner.Y)
                   < _range * 16f && agent.Owner.HasComponent<ComponentAttack>();
        }

        public void Perform(Agent agent, GameTime gameTime)
        {
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
        }
    }
}