using Hevadea.Framework;
using Hevadea.Items;
using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.Tiles.Components
{
    public class DroppableTile : TileComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public DroppableTile()
        {
        }

        public DroppableTile(params Drop[] items)
        {
            foreach (var item in items) Items.Add(item);
        }

        public void Drop(Coordinates coords, Level level)
        {
            foreach (var d in Items)
                if (Rise.Rnd.NextDouble() < d.Chance)
                    d.Item.Drop(level, coords, Rise.Rnd.Next(d.Min, d.Max + 1));
        }
    }
}