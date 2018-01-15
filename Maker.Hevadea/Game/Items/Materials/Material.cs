using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Items.Materials
{
#pragma warning disable IDE1006 // Styles d'affectation de noms
    public interface Material
#pragma warning restore IDE1006 // Styles d'affectation de noms
    {
        float GetAttackBonus(Entity target);
        float GetAttackBonus(Tile target);
    }
}
