using System;
using Hevadea.Game.Items;

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
    
    public class Interactable : EntityComponent
    {
        public delegate void OnInteractHandle(object sender, InteractEventArg e);

        public event OnInteractHandle OnInteracte;

        public void Interacte(Entity entity, Direction attackDirection, Item item = null)
        {
            OnInteracte?.Invoke(this, new InteractEventArg(entity, attackDirection, item));
        }
    }
}