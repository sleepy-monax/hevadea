using System.Collections.Generic;
using Hevadea.Framework;
using Hevadea.Game.Items;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Dropable : EntityComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public void Drop()
        {
            var pos = Owner.GetTilePosition();

            foreach (var d in Items)
                if (Rise.Random.NextDouble() < d.Chance) d.Item.Drop(Owner.Level, pos, Rise.Random.Next(d.Min, d.Max));
        }
    }
}