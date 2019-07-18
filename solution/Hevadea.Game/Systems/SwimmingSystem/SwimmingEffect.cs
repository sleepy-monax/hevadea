using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.SwimmingSystem
{
    public class SwimmingEffect : EntityDrawSystem
    {
        public static readonly Point Size = new Point(16, 8);

        public SwimmingEffect()
        {
            Filter.AllOf(typeof(ComponentSwim));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            if (entity.IsSwimming() && entity.GetComponent<ComponentSwim>().ShowAnimation)
            {
                var frame = (int) (gameTime.TotalGameTime.TotalSeconds * 4) % 4;

                var source = new Rectangle(new Point(16 * frame, 0), Size);
                var position = entity.Position - Size.ToVector2() / 2;

                pool.Tiles.Draw(Resources.ImgWater, position, source, Color.White);
            }
        }
    }
}