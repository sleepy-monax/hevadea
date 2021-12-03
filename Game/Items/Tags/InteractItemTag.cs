using Hevadea.Entities;
using Hevadea.Tiles;

namespace Hevadea.Items.Tags
{
    public abstract class InteractItemTag : ItemTag
    {
        public abstract void InteracteOn(Entity user, Coordinates pos);
    }
}