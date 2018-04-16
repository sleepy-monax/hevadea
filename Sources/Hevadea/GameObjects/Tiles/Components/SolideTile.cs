using Hevadea.GameObjects.Entities;

namespace Hevadea.GameObjects.Tiles.Components
{
    public class SolideTile : TileComponent
    {
        public virtual bool CanPassThrought(Entity entity)
        {
            return false;
        }
    }
}
