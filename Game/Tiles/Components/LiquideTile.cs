using Hevadea.Entities;
using Hevadea.Entities.Components;

namespace Hevadea.Tiles.Components
{
    public class LiquideTile : SolideTile
    {
        public override bool CanPassThrought(Entity entity)
        {
            return entity.HasComponent<ComponentSwim>();
        }
    }
}