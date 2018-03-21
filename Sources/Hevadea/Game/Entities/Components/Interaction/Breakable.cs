using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Items;

namespace Hevadea.Game.Entities.Components.Interaction
{
    public class Breakable : EntityComponent
    {
        private bool DropInventory { get; set; } = false;
        public void Break(Item item = null)
        {
            Owner.Get<Dropable>()?.Drop();
            if (DropInventory) Owner.Get<Inventory>().Content.DropOnGround(Owner.Level, Owner.X, Owner.Y);
            Owner.Remove();
        }
    }
}