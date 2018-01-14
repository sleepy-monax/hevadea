using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Items;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class InteractableComponent : EntityComponent
    {
        public delegate void OnInteractHandle(object sender, InteractEventArg e);
        public event OnInteractHandle OnInteracte;

        public virtual void Interacte(Entity entity, Direction attackDirection, Item item = null)
        {
            OnInteracte?.Invoke(this, new InteractEventArg(entity, attackDirection, item));
        }

    }
}
