using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Ai.Actions
{
    public class ActionWait : IAction
    {
        private double _time = 0f;

        public ActionWait(double time)
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
            _time -= gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
