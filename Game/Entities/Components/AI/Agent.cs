using System.Collections.Generic;
using Hevadea.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.AI
{
    public enum AgentAbortReason
    {
        None,
        ImStuck,
        PickedUp,
        TagetLost
    }

    public class Agent : EntityComponent, IEntityComponentUpdatable, IEntityComponentOverlay
    {
        private Behavior _behavior;

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
            Logger.Log<Agent>($"{Owner.Identifier} aborted: {why}");
            _behavior?.IaAborted(why);
        }

        public void Update(GameTime gameTime)
        {
            _behavior?.Update(gameTime);

            if (!CurrentAction?.IsStillRunning(this) ?? true)
            {
                CurrentAction = null;

                if (ActionQueue.Count > 0)
                {
                    CurrentAction = ActionQueue.Dequeue();

                    if (ActionQueue.Count == 0)
                        _behavior?.IaFinish();
                }
            }

            CurrentAction?.Perform(this, gameTime);
        }

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Rise.Debug.GAME)
            {
                CurrentAction?.DrawDebugInfo(this, spriteBatch);

                foreach (var a in ActionQueue) a.DrawDebugInfo(this, spriteBatch);

                _behavior.DrawDebug(spriteBatch, gameTime);
            }
        }
    }
}