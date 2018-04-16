using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components;

namespace Hevadea.GameObjects.Tiles.Components
{
    public class LiquideTile : SolideTile
    {
        public override bool CanPassThrought(Entity entity)
        {
            return entity.HasComponent<Swim>();
        }
    }
}
