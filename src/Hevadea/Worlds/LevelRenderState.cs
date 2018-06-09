using Hevadea.GameObjects.Entities;
using Microsoft.Xna.Framework;

namespace Hevadea.Worlds
{
    public class RenderState
    {
        public Point RenderBegin;
        public Point RenderEnd;

        public EntityColection OnScreenEntities = new EntityColection();
        public EntityColection AliveEntities = new EntityColection();

        public RenderState(Point renderBegin, Point renderEnd, EntityColection onScreenEntities, EntityColection aliveEntities)
        {
            RenderBegin = renderBegin;
            RenderEnd = renderEnd;

            OnScreenEntities = onScreenEntities;
            AliveEntities = aliveEntities;
        }
    }
}