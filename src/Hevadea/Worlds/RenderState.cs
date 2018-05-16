using Hevadea.GameObjects.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Worlds
{
    public class RenderState
    {
        public Point RenderBegin;
        public Point RenderEnd;
		public EntityColection EntityOnScreen = new EntityColection();
		public EntityColection AliveEntities = new EntityColection();

		public RenderState(Point renderBegin, Point renderEnd, EntityColection entityOnScreen, EntityColection aliveEntities)
		{
			RenderBegin = renderBegin;
			RenderEnd = renderEnd;
		}
    }
}