using System;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.GameObjects.Items.Tags
{
    public class ActionItemTag : InteractItemTag
    {
        public Action<Entity, TilePosition> Action;

        public override void InteracteOn(Entity user, TilePosition pos)
        {
			Action.Invoke(user, pos);
        }
    }
}
