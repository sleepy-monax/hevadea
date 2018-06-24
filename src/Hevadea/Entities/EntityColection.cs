using Hevadea.Registry;
using Hevadea.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Entities
{
    public class EntityColection : List<Entity>
    {
        public void SortForRender()
        {
            Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));
        }
    }
}