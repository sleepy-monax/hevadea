using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.Game.Items.Materials
{
    public class BaseMaterial : Material
    {
        public float Bonus;

        public BaseMaterial(float bonus)
        {
            Bonus = bonus;
        }

        public float GetAttackBonus(Entity target)
        {
            return Bonus;
        }

        public float GetAttackBonus(Tile target)
        {
            return Bonus;
        }
    }
}