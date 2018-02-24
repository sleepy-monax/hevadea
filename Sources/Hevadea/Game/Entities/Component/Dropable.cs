using Hevadea.Game.Items;
using Maker.Rise;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Component
{
    public class Dropable : EntityComponent
    {
        public List<(Item Item, int Min, int Max)> Items { get; set; } = new List<(Item, int, int)>();

        public void Drop()
        {
            var pos = Owner.GetTilePosition();

            foreach (var d in Items) d.Item.Drop(Owner.Level, pos, Engine.Random.Next(d.Min, d.Max));
        }
    }
}