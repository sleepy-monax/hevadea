using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Tiles;
using Maker.Rise.Ressource;
using System.Collections.Generic;

namespace Hevadea.Game.Items
{
    public class TileItem : Item
    {
        private readonly Tile _tile;
        private List<Tile> CanBePlaceOn { get; set; } = new List<Tile>();
        public TileItem(string name, Tile tile, Sprite sprite) : base(name, sprite)
        {
            _tile = tile;
        }
        
        public override void InteracteOn(Entity user, TilePosition pos)
        {
            if (user.Level.GetEntityOnTile(pos).Count == 0)
            {
                var inventory = user.Components.Get<Inventory>();

                inventory?.Content.Remove(this, 1);

                if (CanBePlaceOn.Count == 0 || CanBePlaceOn.Contains(user.Level.GetTile(pos)))
                    user.Level.SetTile(pos, _tile);

                if (user is PlayerEntity p && p.Components.Get<Inventory>().Content.Count(p.HoldingItem) == 0)
                    p.HoldingItem = null;
            }
            else
            {
                base.InteracteOn(user, pos);
            }
        }
    }
}