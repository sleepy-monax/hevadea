using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Items;

namespace Hevadea.Game.Entities.Components.Interaction
{
    public class Breakable : EntityComponent
    {
        private bool DropInventory { get; set; } = false;
        public void Break(Item item = null)
        {
            Owner.GetComponent<Dropable>()?.Drop();
            if (DropInventory) Owner.GetComponent<Inventory>().Content.DropOnGround(Owner.Level, Owner.X, Owner.Y);
            Owner.Remove();
        }
    }
}