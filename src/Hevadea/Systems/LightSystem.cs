using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Systems
{
    public class LightSystem : GameSystem, IEntityRenderSystem
    {
        public LightSystem()
        {
            Filter.AnyOf(typeof(LightSource), typeof(Pickup));
        }

        public void Render(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            LightSource light = entity.GetComponent<LightSource>();

            if (entity.HasComponent<Pickup>(out var pickup) && pickup.PickupedEntity != null)
            {
                light = pickup.PickupedEntity.GetComponent<LightSource>();
            }

            if (light != null && light.IsOn)
            {
                DrawLight(pool.Lights, entity.X, entity.Y, light.Power, light.Color);
            }
        }

        public static void DrawLight(SpriteBatch spriteBatch, float x, float y, float power, Color color)
        {
            spriteBatch.Draw(Ressources.ImgLight, x - power, y - power, power * 2, power * 2, color);
        }
    }
}
