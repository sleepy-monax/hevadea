using Hevadea.Framework;
using Hevadea.Items;
using System.Collections.Generic;

namespace Hevadea.Entities.Components
{
    public class ComponentDropable : EntityComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public void Drop()
        {
            var pos = Owner.Coordinates;

            foreach (var i in Items)
                if (Rise.Rnd.NextDouble() < i.Chance)
                    i.Item.Drop(Owner.Level, pos, Rise.Rnd.Next(i.Min, i.Max));
        }
    }
}