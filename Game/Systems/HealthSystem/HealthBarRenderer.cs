using Hevadea.Entities;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System;
using Hevadea.Entities.Components;

namespace Hevadea.Systems.HealthSystem
{
    public class HealthBarRenderer : EntityDrawSystem
    {
        public const float HEALTH_BAR_WIDTH = 24f;
        public const float HEALTH_BAR_HEIGHT = 2f;

        public HealthBarRenderer()
        {
            Filter.AllOf(typeof(ComponentHealth)).NoneOf(typeof(ComponentPlayerBody));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            var health = Mathf.Max(entity.GetComponent<ComponentHealth>().ValuePercent, 0.05f);

            if (health < 0.95f)
            {
                var barPosition = entity.Position + new Vector2(0, 4) -
                                  new Vector2(HEALTH_BAR_WIDTH / 2, HEALTH_BAR_HEIGHT / 2);
                var barBound = new Vector2(HEALTH_BAR_WIDTH * health, HEALTH_BAR_HEIGHT);

                pool.Overlay.FillRectangle(barPosition + new Vector2(1f, 1f), barBound, Color.Black * 0.45f);

                var red = (int) Math.Sqrt(255 * 255 * (1 - health));
                var green = (int) Math.Sqrt(255 * 255 * health);

                pool.Overlay.FillRectangle(barPosition, barBound, new Color(red, green, 0));
                pool.Overlay.Draw(Resources.ImgHealthbar, barPosition - new Vector2(3),
                    Resources.ImgHealthbar.Bounds.Size.ToVector2(), Color.White);
            }
        }
    }
}