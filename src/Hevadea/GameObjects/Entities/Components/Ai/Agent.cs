using Hevadea.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities.Components.Ai
{
    public enum AgentAbortReason { None, ImStuck, PickedUp, TagetLost }

    public class Agent : EntityComponent, IEntityComponentUpdatable, IEntityComponentDrawableOverlay
    {
        Behavior _behavior;

        public Queue<IAction> ActionQueue { get; } = new Queue<IAction>();
        public IAction CurrentAction { get; private set; }

        public Agent(Behavior behavior)
        {
            _behavior = behavior;
            _behavior.Agent = this;
        }

        public bool IsBusy()
        {
            return CurrentAction != null || ActionQueue.Count > 0;
        }

        public void Flush()
        {
            CurrentAction = null;
            ActionQueue.Clear();
        }

        public void Abort(AgentAbortReason why)
        {
            CurrentAction = null;
            ActionQueue.Clear();
            Logger.Log<Agent>($"{Owner.GetIdentifier()} aborted: {why}");
            _behavior?.IaAborted(why);
        }

        public void Update(GameTime gameTime)
        {
            _behavior?.Update(gameTime);

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
                        _behavior?.IaFinish();
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

                _behavior.DrawDebug(spriteBatch, gameTime);
            }
        }
    }
}