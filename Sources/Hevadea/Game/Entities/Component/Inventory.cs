using Hevadea.Game.Items;
using Hevadea.Game.Storage;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Component
{
    public class Inventory : EntityComponent, ISaveLoadComponent
    {
        private Item _lastAdded;

        public Inventory(int slotCount)
        {
            Content = new ItemStorage(slotCount);
        }

        public ItemStorage Content { get; }
        public bool AlowPickUp { get; set; } = false;

        public void OnGameSave(EntityStorage store)
        {
            store.Set(nameof(Content), Content.Items);
        }

        public void OnGameLoad(EntityStorage store)
        {
            var l = (Dictionary<string,object>)store.Get(nameof(Content), new Dictionary<string,object>());
            foreach (var i in l)
            {
                Content.Items.Add(int.Parse(i.Key), (int)i.Value);
            }
        }

        public bool Pickup(Item item)
        {
            if (AlowPickUp && Content.Add(item))
            {
                _lastAdded = item;
                return true;
            }

            return false;
        }
    }
}