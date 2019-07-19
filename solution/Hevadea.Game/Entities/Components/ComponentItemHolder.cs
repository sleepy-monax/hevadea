using Hevadea.Items;
using Hevadea.Registry;
using Hevadea.Storage;

namespace Hevadea.Entities.Components
{
    public class ComponentItemHolder : EntityComponent, IEntityComponentSaveLoad
    {
        public Item HoldedItem { get; set; } = null;

        public void OnGameLoad(EntityStorage store)
        {
            var item_name = store.ValueOf(nameof(HoldedItem), "null");
            HoldedItem = ITEMS.ByName.TryGetValue(item_name, out var item) ? item : null;
        }

        public void OnGameSave(EntityStorage store)
        {
            store.Value(nameof(HoldedItem), HoldedItem?.Name ?? "null");
        }
    }
}