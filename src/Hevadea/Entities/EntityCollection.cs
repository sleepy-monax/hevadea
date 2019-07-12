using System.Collections.Generic;

namespace Hevadea.Entities
{
    public class EntityCollection : List<Entity>
    {
        public void SortForRender()
        {
            Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));
        }
    }
}