using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework.Extension;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.LightingSystem
{
    public class ShadowSystem : EntityDrawSystem
    {
        public ShadowSystem()
        {
            Filter.AllOf(typeof(ComponentCastShadow));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            if (entity.HasComponent<ComponentCastShadow>(out var shadow) &&
               !(entity.GetComponent<ComponentSwim>()?.IsSwiming ?? false))
                pool.Shadows.Draw(Resources.ImgShadow,
                    new Vector2(entity.X - 7 * shadow.Scale, entity.Y - 3f * shadow.Scale),
                    new Vector2(14, 6) * shadow.Scale, Color.White);
        }
    }
}