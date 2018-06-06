using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Ai
{
    public interface IBehavior
    {
        void IaAborted(Agent agent, AgentAbortReason why);

        void IaFinish(Agent agent);

        void Update(Agent agent, GameTime gameTime);
        void DrawDebug(SpriteBatch spriteBatch, Agent agent, GameTime gameTime);
    }
}