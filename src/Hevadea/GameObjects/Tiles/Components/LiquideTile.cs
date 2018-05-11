using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.States;

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
