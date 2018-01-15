using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Items.Tools
{
    public class ToolType
    {
        string Name;

        public ToolType(string name)
        {
            Name = name;
        }

        public virtual float GetAttackBonus(Entity target)
        {
            return 1f;
        }

        public virtual float GetAttackBonus(Tile target)
        {
            return 1f;
        }

    }
}
