using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Ai.Actions;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Ai.Behaviors
{
    public class BehaviorEnemy : BehaviorAnimal
    {
        public float AgroRange { get; set; } = 5f;
        public float FollowRange { get; set; } = 16f;
        public Entity Target { get; private set; } = null;
        public float ChanceToAgro { get; set; } = 0.5f;
        
        public new void Update(Agent agent, GameTime gameTime)
        {   
            if (Target != null &&
                Target.Level == agent.Owner.Level &&
                Mathf.Distance(agent.Owner.Position, Target.Position) > FollowRange)
            {
                agent.Abort(AgentAbortReason.TagetLost);
                Target = null; 
            }
            
            if (!agent.IsBusy())
            {
                
                if (Target == null)
                {
                    var game = agent.Owner.Game;
                    
                    if (game.MainPlayer != null &&
                        game.MainPlayer.Level == agent.Owner.Level &&
                        Mathf.Distance(game.MainPlayer.Position, agent.Owner.Position) < AgroRange &&
                        Rise.Rnd.NextFloat() < ChanceToAgro)
                    {
                        Target = game.MainPlayer;
                    }
                }

                if (Target != null)
                {
                    agent.MoveTo(Target.GetTilePosition(), MoveSpeed, false, (int)(FollowRange + 4));
                }
                
            }
            
            //if (!agent.IsBusy())
            //    base.Update(agent, gameTime);                
        }
    }
}