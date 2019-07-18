using Hevadea.Entities;
using Hevadea.Tiles;
using System;

namespace Hevadea.Items.Tags
{
    public class ActionItemTag : InteractItemTag
    {
        public Action<Entity, Coordinates> Action;

        public override void InteracteOn(Entity user, Coordinates pos)
        {
            Action.Invoke(user, pos);
        }
    }
}