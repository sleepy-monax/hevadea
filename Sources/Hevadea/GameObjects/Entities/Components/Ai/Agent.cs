using System.Collections.Generic;
using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Ai
{
    public enum AgentAbortReason { None, ImStuck, Pickup}

    public class Agent : Component
    {
        public static int IdCounter = 0;
        public Queue<IAction> ActionQueue { get; } = new Queue<IAction>();
        public IAction CurrentAction { get; private set; }
        public IBehavior Behavior { get; set; }
        
        public bool IsBusy()
        {
            return CurrentAction != null || ActionQueue.Count > 0;
        }

        public void Abort(AgentAbortReason why)
        {
            CurrentAction = null;
            ActionQueue.Clear();
            Logger.Log<Agent>($"{Entity.Ueid} aborted: {why}");
            Behavior?.IaAborted(this, why);
        }
        
        public override void Update(GameTime gt)
        {
            Behavior?.Update(this, gt);

            if (CurrentAction != null && CurrentAction.IsStillRunning(this))
            {
                CurrentAction.Perform(this, gt);
            }
            else
            {
                CurrentAction = null;
                
                if (ActionQueue.Count > 0)
                {
                    CurrentAction = ActionQueue.Dequeue();

                    if (ActionQueue.Count == 0)
                        Behavior?.IaFinish(this);
                }
            }
        }

        public override void Draw(SpriteBatch sb, GameTime gt)
        {
            if (Rise.ShowDebug)
            {    
                CurrentAction?.DrawDebugInfo(this, sb);
                
                foreach (var a in ActionQueue)
                {
                    a.DrawDebugInfo(this, sb);
                }
            }
        }
    }
}