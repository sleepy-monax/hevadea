using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.Game.Items.Materials
{
    public interface Material
    {
        float GetAttackBonus(Entity target);
        float GetAttackBonus(Tile target);
    }
}