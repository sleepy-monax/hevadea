using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities
{
    public class EntityColection : List<Entity>
    {
        public void SortForRender()
        {
            Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));
        }

        public void UpdateAll(GameTime gameTime)
        {
            ForEach(e => e.Update(gameTime));
        }
    }
}