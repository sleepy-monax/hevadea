using System.Collections.Generic;
using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Ai
{
    public enum AgentAbortReason { None, ImStuck, PickedUp, TagetLost }

    public class Agent: EntityComponent, IEntityComponentUpdatable, IEntityComponentDrawableOverlay
    {
        public Queue<IAction> ActionQueue { get; } = new Queue<IAction>();
        public IAction CurrentAction { get; private set; }
        
        public IBehavior Behavior { get; set; }

        public Agent()
        {
            
        }

        public bool IsBusy()
        {
            return CurrentAction != null || ActionQueue.Count > 0;
        }

        public void Abort(AgentAbortReason why)
        {
            CurrentAction = null;
            ActionQueue.Clear();
            Logger.Log<Agent>($"{Owner.GetIdentifier()} aborted: {why}");
            Behavior?.IaAborted(this, why);
        }
        
        public void Update(GameTime gameTime)
        {
            Behavior?.Update(this, gameTime);

            if (CurrentAction != null && CurrentAction.IsStillRunning(this))
            {
                CurrentAction.Perform(this, gameTime);
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

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (Rise.ShowDebug)
            {    
                CurrentAction?.DrawDebugInfo(this, spriteBatch);
                
                foreach (var a in ActionQueue)
                {
                    a.DrawDebugInfo(this, spriteBatch);
                }
            }
        }
    }
}