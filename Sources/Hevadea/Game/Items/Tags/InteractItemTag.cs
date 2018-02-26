using Hevadea.Game.Entities;
using Hevadea.Game.Tiles;

namespace Hevadea.Game.Items.Tags
{
    public abstract class InteractItemTag : ItemTag
    {
        public abstract void InteracteOn(Entity user, TilePosition pos);
    }
}