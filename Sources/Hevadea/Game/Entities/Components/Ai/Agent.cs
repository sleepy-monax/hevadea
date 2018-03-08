using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using Hevadea.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Ai
{
    public class Agent: EntityComponent, IEntityComponentUpdatable, IEntityComponentDrawableOverlay
    {
        public Queue<IAction> ActionQueue { get; } = new Queue<IAction>();
        public IAction CurrentAction { get; private set; }

        public bool IsBusy()
        {
            return CurrentAction != null || ActionQueue.Count > 0;
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
                    CurrentAction = ActionQueue.Dequeue();
                
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