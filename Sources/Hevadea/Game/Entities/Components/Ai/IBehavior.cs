using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Ai
{
    public interface IBehavior
    {
        void IaAborted(Agent agent, AgentAbortReason why);
        void Update(Agent agen, GameTime gameTime);
    }
}