using System.Collections.Generic;
using Hevadea.Game;
using Hevadea.Game.Worlds;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.GameObjects.Items.Tags
{
    public abstract class PlacableItemTag : InteractItemTag
    {
        public List<Tile> CanBePlaceOn { get; set; } = new List<Tile>();
        public bool ConsumeItem { get; set; } = true;
        
        public override void InteracteOn(Entity user, TilePosition pos)
        {
            var inventory = user.GetComponent<Inventory>();
            var level = user.Level;
            
            if (user.Level.GetEntityOnTile(pos).Count == 0 &&
               (CanBePlaceOn.Count == 0 ||
                CanBePlaceOn.Contains(level.GetTile(pos))))
            {
                inventory?.Content.Remove(AttachedItem, 1);

                Place(level, pos, user.Facing);

                if (ConsumeItem && user is EntityPlayer p && p.GetComponent<Inventory>().Content.Count(p.HoldingItem) == 0)
                    p.HoldingItem = null;
            }
        }

        public abstract void Place(Level level, TilePosition tile, Direction facing);
    }
}