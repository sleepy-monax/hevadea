using Hevadea.Framework;
using Hevadea.GameObjects.Items;
using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Tiles.Components
{
    public class DroppableTile : TileComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public DroppableTile()
        {
        }

        public DroppableTile(params Drop[] items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public void Drop(TilePosition position, Level level)
        {
            foreach (var d in Items)
                if (Rise.Rnd.NextDouble() < d.Chance) d.Item.Drop(level, position, Rise.Rnd.Next(d.Min, d.Max + 1));
        }
    }
}