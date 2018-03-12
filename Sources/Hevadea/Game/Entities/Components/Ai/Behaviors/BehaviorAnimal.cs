using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Registry;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Ai.Behaviors
{
    public class BehaviorAnimal : IBehavior
    {
        public void Update(Agent agent, GameTime gameTime)
        {
            if (!agent.IsBusy())
            {
                var dx = Rise.Random.NextValue(-1, 1);
                var dy = Rise.Random.Next(-1, 1);
                
                if ((dx != 0 || dy !=0) && Level.GetTile((int)(X+dx)/16,(int)(Y+dy)/16) == TILES.WATER)
                {
                    agent.ActionQueue.Enqueue(new Mo);
                }
            }
        }
        
        public void IaAborted(Agent agent, AgentAbortReason why)
        {
          
        }

        public void IaFinish(Agent agent)
        {
            
        }
    }
}