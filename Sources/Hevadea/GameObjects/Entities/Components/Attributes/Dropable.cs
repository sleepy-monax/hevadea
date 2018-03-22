using System.Collections.Generic;
using Hevadea.Framework;
using Hevadea.GameObjects.Items;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Dropable : Component
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public void Drop()
        {
            var pos = Entity.GetTilePosition();

            foreach (var d in Items)
                if (Rise.Rnd.NextDouble() < d.Chance) d.Item.Drop(Entity.Level, pos, Rise.Rnd.Next(d.Min, d.Max));
        }
    }
}