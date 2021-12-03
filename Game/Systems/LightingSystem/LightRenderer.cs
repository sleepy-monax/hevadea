using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework.Extension;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Systems.LightingSystem
{
    public class LightSystem : EntityDrawSystem
    {
        public LightSystem()
        {
            Filter.AnyOf(typeof(ComponentLightSource), typeof(ComponentPickup));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            var light = entity.GetComponent<ComponentLightSource>();

            if (entity.HasComponent<ComponentPickup>(out var pickup) && pickup.PickedUpEntity != null)
                if (pickup.PickedUpEntity.HasComponent<ComponentLightSource>(out var pickupLight))
                    light = pickupLight;

            if (light != null && light.IsOn) DrawLight(pool.Lights, entity.X, entity.Y, light.Power, light.Color);
        }

        public static void DrawLight(SpriteBatch spriteBatch, float x, float y, float power, Color color)
        {
            spriteBatch.Draw(Resources.ImgLight, x - power, y - power, power * 2, power * 2, color);
        }
    }
}