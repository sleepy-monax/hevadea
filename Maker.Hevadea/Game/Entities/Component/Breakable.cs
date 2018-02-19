using Maker.Hevadea.Game.Items;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Breakable : EntityComponent
    {
        private bool DropInventory { get; set; } = false;
        public void Break(Item item = null)
        {
            Owner.Components.Get<Dropable>()?.Drop();
            if (DropInventory) Owner.Components.Get<Inventory>().Content.DropOnGround(Owner.Level, Owner.X + Owner.Origin.X, Owner.Y + Owner.Origin.Y);
            Owner.Remove();
        }
    }
}