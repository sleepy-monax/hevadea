using System.Collections.Generic;
using Hevadea.GameObjects.Entities;
using Microsoft.Xna.Framework;

namespace Hevadea.Worlds
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