using Hevadea.Game.Entities;
using Hevadea.Game.Tiles;

namespace Hevadea.Game.Items.Materials
{
    public interface Material
    {
        float GetAttackBonus(Entity target);
        float GetAttackBonus(Tile target);
    }
}