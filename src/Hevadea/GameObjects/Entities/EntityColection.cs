using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
{
    public class EntityColection : List<Entity>
    {
        public EntityColection()
        {
        }

        public void SortForRender()
        {
            this.Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));
        }

        public void UpdateAll(GameTime gameTime)
        {
            foreach (var e in this)
            {
                e.Update(gameTime);
            }
        }
        
        public void DrawAll()
        {
            
        }

        public void DrawLights()
        {
            
        }

        public void DrawShadow()
        {
            
        }
    }
}
