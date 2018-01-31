using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;

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
