using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Game.Entities;
using Hevadea.Game.Tiles;

namespace Hevadea.Game.Items.Tags
{
    public class ActionItemTag : InteractItemTag
    {
        public Action<Entity, TilePosition> Action;

        public override void InteracteOn(Entity user, TilePosition pos)
        {
            Action?.Invoke(user, pos);
        }
    }
}
