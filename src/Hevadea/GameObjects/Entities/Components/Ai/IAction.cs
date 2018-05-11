using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Ai
{
    public interface IAction
    {
        bool IsStillRunning(Agent agent);

        void Perform(Agent agent, GameTime gameTime);

        void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch);
    }
}