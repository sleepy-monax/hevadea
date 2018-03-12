using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using System.Collections.Generic;

namespace Hevadea.Game.Items.Tags
{
    public abstract class PlacableItemTag : InteractItemTag
    {
        public List<Tile> CanBePlaceOn { get; set; } = new List<Tile>();
        public bool ConsumeItem { get; set; } = true;
        
        public override void InteracteOn(Entity user, TilePosition pos)
        {
            var inventory = user.Get<Inventory>();
            var level = user.Level;
            
            if (user.Level.GetEntityOnTile(pos).Count == 0 &&
               (CanBePlaceOn.Count == 0 ||
                CanBePlaceOn.Contains(level.GetTile(pos))))
            {
                inventory?.Content.Remove(AttachedItem, 1);

                Place(level, pos, user.Facing);

                if (ConsumeItem && user is EntityPlayer p && p.Get<Inventory>().Content.Count(p.HoldingItem) == 0)
                    p.HoldingItem = null;
            }
        }

        public abstract void Place(Level level, TilePosition tile, Direction facing);
    }
}