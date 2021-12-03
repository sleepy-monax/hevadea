using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.AI.Actions
{
    public class ActionWait : IAction
    {
        private float _time = 0f;

        public ActionWait(float time)
        {
            _time = time;
        }

        public void DrawDebugInfo(Agent agent, SpriteBatch spriteBatch)
        {
        }

        public bool IsStillRunning(Agent agent)
        {
            return _time > 0;
        }

        public void Perform(Agent agent, GameTime gameTime)
        {
            _time -= gameTime.GetDeltaTime();
        }
    }
}