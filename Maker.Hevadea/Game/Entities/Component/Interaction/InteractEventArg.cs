using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Items;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class InteractEventArg : EventArgs
    {
        public readonly Entity Entity;
        public readonly Direction Direction;
        public readonly Item Item;

        public InteractEventArg(Entity entity, Direction direction, Item item)
        {
            this.Entity = entity;
            this.Direction = direction;
            this.Item = item;
        }
    }
}
