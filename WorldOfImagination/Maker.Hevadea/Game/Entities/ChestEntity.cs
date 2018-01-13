using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Menus;

namespace Maker.Hevadea.Game.Entities
{
    public class ChestEntity : Entity
    {
        public ChestEntity()
        {
             AddComponent(new InventoryComponent(512));
             AddComponent(new InteractableComponent());
            GetComponent<InteractableComponent>().OnInteracte += (sender, arg) =>
            {
                if (arg.Entity.HasComponent<InventoryComponent>())
                {
                    Game.SetMenu(new ChestMenu(arg.Entity, this, World, Game));
                }
            };
        }
    }
}