using System.Collections.Generic;
using Maker.Hevadea.Game.Entities;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game
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