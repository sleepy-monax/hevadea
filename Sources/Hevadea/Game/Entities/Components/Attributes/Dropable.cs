using Hevadea.Framework;
using Hevadea.Game.Items;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Dropable : EntityComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public void Drop()
        {
            var pos = Owner.GetTilePosition();

            foreach (var d in Items)
                if (Rise.Rnd.NextDouble() < d.Chance) d.Item.Drop(Owner.Level, pos, Rise.Rnd.Next(d.Min, d.Max));
        }
    }
}