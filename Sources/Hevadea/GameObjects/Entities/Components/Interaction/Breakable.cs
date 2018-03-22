using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;

namespace Hevadea.GameObjects.Entities.Components.Interaction
{
    public class Breakable : Component
    {
        private bool DropInventory { get; set; } = false;
        public void Break(Item item = null)
        {
            Entity.GetComponent<Dropable>()?.Drop();
            if (DropInventory) Entity.GetComponent<Inventory>().Content.DropOnGround(Entity.Level, Entity.X, Entity.Y);
            Entity.Remove();
        }
    }
}