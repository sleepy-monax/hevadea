using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Items;

namespace Hevadea.Game.Entities.Components.Interaction
{
    public class Breakable : EntityComponent
    {
        private bool DropInventory { get; set; } = false;
        public void Break(Item item = null)
        {
            AttachedEntity.Get<Dropable>()?.Drop();
            if (DropInventory) AttachedEntity.Get<Inventory>().Content.DropOnGround(AttachedEntity.Level, AttachedEntity.X, AttachedEntity.Y);
            AttachedEntity.Remove();
        }
    }
}