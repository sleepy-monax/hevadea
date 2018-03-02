using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Items;

namespace Hevadea.Game.Entities.Component
{
    public class Breakable : EntityComponent
    {
        private bool DropInventory { get; set; } = false;
        public void Break(Item item = null)
        {
            Owner.Get<Dropable>()?.Drop();
            if (DropInventory) Owner.Get<Inventory>().Content.DropOnGround(Owner.Level, Owner.X + Owner.Origin.X, Owner.Y + Owner.Origin.Y);
            Owner.Remove();
        }
    }
}