using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hevadea.GameObjects.Entities;

namespace Hevadea.Game.Worlds
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