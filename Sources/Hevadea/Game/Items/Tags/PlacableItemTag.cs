using System.Collections.Generic;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;

namespace Hevadea.Game.Items.Tags
{
    public abstract class PlacableItemTag : InteractItemTag
    {
        public List<Tile> CanBePlaceOn { get; set; } = new List<Tile>();
        
        public override void InteracteOn(Entity user, TilePosition pos)
        {
            var inventory = user.Get<Inventory>();
            var level = user.Level;
            
            if (user.Level.GetEntityOnTile(pos).Count == 0 && (CanBePlaceOn.Count == 0 || CanBePlaceOn.Contains(level.GetTile(pos))))
            {
                inventory?.Content.Remove(AttachedItem, 1);

                Place(level, pos, user.Facing);
                
                if (user is EntityPlayer p)
                    if (p.Get<Inventory>().Content.Count(p.HoldingItem) == 0)
                        p.HoldingItem = null;
            }
        }

        public abstract void Place(Level level, TilePosition tile, Direction facing);
    }
}