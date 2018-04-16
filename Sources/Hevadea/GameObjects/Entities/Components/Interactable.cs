using System;
using Hevadea.GameObjects.Items;
using Hevadea.Utils;

namespace Hevadea.GameObjects.Entities.Components
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

        public event OnInteractHandle Interacted;

        public void Interacte(Entity entity, Direction attackDirection, Item item = null)
        {
            Interacted?.Invoke(this, new InteractEventArg(entity, attackDirection, item));
        }
    }
}