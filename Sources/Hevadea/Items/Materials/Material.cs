using Hevadea.Entities;
using Hevadea.Tiles;

namespace Hevadea.Items.Materials
{
    public interface Material
    {
        float GetAttackBonus(Entity target);
        float GetAttackBonus(Tile target);
    }
}