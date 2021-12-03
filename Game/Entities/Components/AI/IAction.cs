using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.AI
{
    public interface IAction
    {
        bool IsStillRunning(Agent agent);

        void Perform(Agent agent, GameTime gameTime);

        void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch);
    }
}