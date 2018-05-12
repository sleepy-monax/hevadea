using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Entities.Components;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Worlds
{
    public partial class Level
    {




        public List<Entity> GetEntitiesOnArea(RectangleF area)
        {
            var result = new List<Entity>();

            var beginX = area.X / GLOBAL.Unit - 1;
            var beginY = area.Y / GLOBAL.Unit - 1;

            var endX = (area.X + area.Width) / GLOBAL.Unit + 1;
            var endY = (area.Y + area.Height) / GLOBAL.Unit + 1;

            for (int x = (int)beginX; x < endX; x++)
                for (int y = (int)beginY; y < endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                    var entities = GetEntitiesAt(x, y);

                    result.AddRange(entities.Where(i => i.GetComponent<Colider>()?.GetHitBox().IntersectsWith(area) ?? area.Contains(i.Position)));
                }

            return result;
        }

        public List<Entity> GetEntitiesOnArea(Rectangle area)
        {
            return GetEntitiesOnArea(new RectangleF(area.X, area.Y, area.Width, area.Height));
        }

        public List<Entity> GetEntitiesOnRadius(float cx, float cy, float radius)
        {
            var entities = GetEntitiesOnArea(new RectangleF(cx - radius, cy - radius, radius * 2, radius * 2));
            var result = new List<Entity>();

            foreach (var e in entities)
            {
                if (Mathf.Distance(e.X, e.Y, cx, cy) <= radius) result.Add(e);
            }

            return result;
        }
    }
}