using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Systems.InventorySystem;
using Hevadea.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Items.Tags
{
    public abstract class PlacableItemTag : InteractItemTag
    {
        public List<Tile> CanBePlaceOn { get; set; } = new List<Tile>();
        public bool ConsumeItem { get; set; } = true;

        public override void InteracteOn(Entity user, Coordinates coords)
        {
            var inventory = user.GetComponent<ComponentInventory>();
            var level = user.Level;

            var e = user.Level.QueryEntity(coords).FirstOrDefault();

            if ((e == null || e.HasComponent<ComponentBreakable>()) &&
                (CanBePlaceOn.Count == 0 ||
                 CanBePlaceOn.Contains(level.GetTile(coords))))
            {
                e?.GetComponent<ComponentBreakable>()?.Break();
                inventory?.Content.Remove(AttachedItem, 1);

                Place(level, coords, user.Facing);

                if (ConsumeItem && user is Player p && p.GetComponent<ComponentInventory>().Content.Count(p.HoldedItem()) == 0)
                    p.HoldItem(null);
            }
        }

        public abstract void Place(Level level, Coordinates tile, Direction facing);
    }
}