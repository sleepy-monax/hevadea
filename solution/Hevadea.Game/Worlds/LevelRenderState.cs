using Hevadea.Entities;
using Microsoft.Xna.Framework;

namespace Hevadea.Worlds
{
    public class RenderState
    {
        public Point RenderBegin;
        public Point RenderEnd;

        public EntityCollection OnScreenEntities = new EntityCollection();
        public EntityCollection AliveEntities = new EntityCollection();

        public RenderState(Point renderBegin, Point renderEnd, EntityCollection onScreenEntities,
            EntityCollection aliveEntities)
        {
            RenderBegin = renderBegin;
            RenderEnd = renderEnd;

            OnScreenEntities = onScreenEntities;
            AliveEntities = aliveEntities;
        }
    }
}