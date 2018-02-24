using Hevadea.Game.Items;
using System;

namespace Hevadea.Game.Entities.Component
{
    public class InteractEventArg : EventArgs
    {
        public readonly Direction Direction;
        public readonly Entity Entity;
        public readonly Item Item;

        public InteractEventArg(Entity entity, Direction direction, Item item)
        {
            Entity = entity;
            Direction = direction;
            Item = item;
        }
    }
}