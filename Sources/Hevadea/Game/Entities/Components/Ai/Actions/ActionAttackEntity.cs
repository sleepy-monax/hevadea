using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Ai.Actions
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
                   Mathf.Distance(_taget.X, _taget.Y, agent.AttachedEntity.X, agent.AttachedEntity.Y) 
                   < _atackRange * 16f && agent.AttachedEntity.Has<Attack>();
        }

        public void Perform(Agent agent, GameTime gameTime)
        {
            
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
            
        }
    }
}