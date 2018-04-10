using System;
using Hevadea.Entities;
using Hevadea.Tiles;

namespace Hevadea.Items.Tags
{
    public class ActionItemTag : InteractItemTag
    {
        public Action<Entity, TilePosition> Action;

        public override void InteracteOn(Entity user, TilePosition pos)
        {
            Action?.Invoke(user, pos);
        }
    }
}
