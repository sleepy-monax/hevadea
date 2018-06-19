using Hevadea.Entities;

namespace Hevadea.Tiles.Components
{
    public class SolideTile : TileComponent
    {
        public virtual bool CanPassThrought(Entity entity)
        {
            return false;
        }
    }
}