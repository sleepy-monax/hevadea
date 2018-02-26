using Hevadea.Game.Items;
using Maker.Rise;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Component
{
    public class Dropable : EntityComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public void Drop()
        {
            var pos = Owner.GetTilePosition();

            foreach (var d in Items)
                if (Engine.Random.NextDouble() < d.Chance) d.Item.Drop(Owner.Level, pos, Engine.Random.Next(d.Min, d.Max));
        }
    }
}