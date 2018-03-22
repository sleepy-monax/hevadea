using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Ai.Actions
{
    public class ActionAttackEntity : IAction
    {
        private Entity _taget;
        private float _atackRange;
        
        public ActionAttackEntity(Entity e)
        {
            _taget = e;
        }
        
        public bool IsStillRunning(Agent agent)
        {
            return !_taget.Removed &&
                   Mathf.Distance(_taget.X, _taget.Y, agent.Entity.X, agent.Entity.Y) 
                   < _atackRange * 16f && agent.Entity.HasComponent<Attack>();
        }

        public void Perform(Agent agent, GameTime gameTime)
        {
            
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
            
        }
    }
}