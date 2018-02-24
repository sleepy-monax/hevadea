using Hevadea.Game.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Game
{
    public class LevelRenderState
    {
        public Point Begin;
        public Point End;
        public List<Entity> OnScreenEntities;

        internal void Clear()
        {
            OnScreenEntities.Clear();
        }
    }
}