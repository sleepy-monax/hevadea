using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.AI
{
    public abstract class Behavior
    {
        public Agent Agent { get; set; }

        public virtual void IaAborted(AgentAbortReason why)
        {
        }

        public virtual void IaFinish()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void DrawDebug(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}