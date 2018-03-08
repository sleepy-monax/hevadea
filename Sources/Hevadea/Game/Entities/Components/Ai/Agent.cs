using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Components.Ai
{
    public enum AgentAbortReason { None, ImStuck}

    public class Agent: EntityComponent, IEntityComponentUpdatable, IEntityComponentDrawableOverlay
    {
        public static int IdCounter = 0;
        public int Id { get; }
        public Queue<IAction> ActionQueue { get; } = new Queue<IAction>();
        public IAction CurrentAction { get; private set; }

        public Agent()
        {
            Id = IdCounter++;
        }

        public bool IsBusy()
        {
            return CurrentAction != null || ActionQueue.Count > 0;
        }

        public void Abort(AgentAbortReason why)
        {
            CurrentAction = null;
            ActionQueue.Clear();
            Logger.Log<Agent>($"{Id} aborted: {why}");
        }
        
        public void Update(GameTime gameTime)
        {
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