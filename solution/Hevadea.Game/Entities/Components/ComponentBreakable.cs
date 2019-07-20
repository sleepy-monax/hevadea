using Hevadea.Items;

namespace Hevadea.Entities.Components
{
    public class ComponentBreakable : EntityComponent
    {
        public bool DropInventory { get; set; } = false;

        public void Break(Item item = null)
        {
            Owner.GetComponent<ComponentDropable>()?.Drop();
            if (DropInventory) Owner.GetComponent<ComponentInventory>().Content.DropOnGround(Owner.Level, Owner.X, Owner.Y);
            Owner.Remove();
        }
    }
}