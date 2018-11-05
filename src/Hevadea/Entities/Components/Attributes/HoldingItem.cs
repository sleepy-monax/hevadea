using System;
using Hevadea.Items;

namespace Hevadea.Entities.Components.Attributes
{
    public class HoldingItem : EntityComponent
    {
        public Item Item { get; set; }

        public HoldingItem()
        {
        }
    }
}
