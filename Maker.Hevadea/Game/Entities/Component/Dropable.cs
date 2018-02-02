using System.Collections.Generic;
using Maker.Hevadea.Game.Items;
using Maker.Rise;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Dropable : EntityComponent
    {
        public List<(Item, int, int)> Items { get; set; } = new List<(Item, int, int)>();

        public void Drop()
        {
            var pos = Owner.GetTilePosition();

            foreach (var d in Items)
            {
                d.Item1.Drop(Owner.Level, pos, Engine.Random.Next(d.Item2, d.Item3));
            }
        }
    }
}
