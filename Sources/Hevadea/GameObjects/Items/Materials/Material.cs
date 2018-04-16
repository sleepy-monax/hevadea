using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.GameObjects.Items.Materials
{
    public interface Material
    {
        float GetAttackBonus(Entity target);
        float GetAttackBonus(Tile target);
    }
}