using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;
using System;

namespace Hevadea.GameObjects.Items.Tags
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