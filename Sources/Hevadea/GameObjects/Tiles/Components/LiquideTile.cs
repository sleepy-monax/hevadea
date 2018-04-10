using Hevadea.Entities;
using Hevadea.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Tiles.Components
{
    public class LiquideTile : SolideTile
    {
        public override bool CanPassThrought(Entity entity)
        {
            return entity.HasComponent<Swim>();
        }
    }
}
