using Hevadea.Entities;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Extension;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class ShadowSystem : EntityDrawSystem
    {
        public ShadowSystem()
        {
            Filter.AllOf(typeof(CastShadow));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            if (entity.HasComponent<CastShadow>(out var shadow) && !(entity.GetComponent<Swim>()?.IsSwiming ?? false))
            {
                pool.Shadows.Draw(Ressources.ImgShadow, new Vector2(entity.X - 7 * shadow.Scale, entity.Y - 3f * shadow.Scale), new Vector2(14, 6) * shadow.Scale, Color.White);
            }
        }
    }
}