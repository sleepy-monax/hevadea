using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.GameObjects.Items.Tags
{
    public abstract class InteractItemTag : ItemTag
    {
        public abstract void InteracteOn(Entity user, TilePosition pos);
    }
}