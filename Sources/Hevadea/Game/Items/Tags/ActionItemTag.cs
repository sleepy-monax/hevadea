using Hevadea.Game.Entities;
using Hevadea.Game.Tiles;
using System;

namespace Hevadea.Game.Items.Tags
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
