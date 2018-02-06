using System;
using Maker.Hevadea.Enums;
using Maker.Hevadea.Game.Items;

namespace Maker.Hevadea.Game.Entities.Component
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
