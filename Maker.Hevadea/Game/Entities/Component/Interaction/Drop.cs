using Maker.Hevadea.Game.Items;
using Maker.Rise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class Dropable : EntityComponent
    {
        public List<(Item, int, int)> Items { get; set; } = new List<(Item, int, int)>();

        public void Drop()
        {
            var pos = Owner.GetTilePosition();
            foreach (var d in Items)
            {
                d.Item1.Drop(Owner.Level, pos, Engine.Random.Next(d.Item2, d.Item2));
            }
        }
    }
}
