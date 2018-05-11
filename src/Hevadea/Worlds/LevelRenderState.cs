using Hevadea.GameObjects.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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