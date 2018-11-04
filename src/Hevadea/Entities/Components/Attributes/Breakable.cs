using Hevadea.Items;

namespace Hevadea.Entities.Components.Attributes
{
    public class Breakable : EntityComponent
    {
        bool DropInventory { get; set; } = false;

        public void Break(Item item = null)
        {
            Owner.GetComponent<Dropable>()?.Drop();
            if (DropInventory) Owner.GetComponent<Inventory>().Content.DropOnGround(Owner.Level, Owner.X, Owner.Y);
            Owner.Remove();
        }
    }
}