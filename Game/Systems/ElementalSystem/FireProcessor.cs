using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.ElementalSystem
{
    public class FireProcessor : EntityUpdateSystem
    {
        public static readonly float FIRE_SPREAD_RANGE = 32f;
        public static readonly float FIRE_DAMAGES = 1f;
        public static readonly float CHANCE_TO_BREAK_FROM_FIRE = 0.01f;

        public FireProcessor()
        {
            Filter.AllOf(typeof(ComponentFlammable));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var fire = entity.GetComponent<ComponentFlammable>();
            var health = entity.GetComponent<ComponentHealth>();

            if (fire.BurnningTimer < 0.01f) fire.Extinguish();
            if (entity.IsSwimming()) fire.Extinguish();

            if (fire.IsBurning)
            {
                // Reduce burning timer
                fire.BurnningTimer -= gameTime.GetDeltaTime();

                // Spread fire
                foreach (var v in entity.GetEntitiesInRadius(FIRE_SPREAD_RANGE))
                    if (v != entity && Rise.Rnd.NextDouble() < 0.005)
                    {
                        v.GetComponent<ComponentFlammable>()?.SetInFire();
                        v.GetComponent<ComponentExplosive>()?.Detonate();
                    }

                // Reduce entity health
                health?.Hurt(entity, FIRE_DAMAGES * gameTime.GetDeltaTime(), false);

                // Break it if possible
                if (Rise.Rnd.NextFloat() <= CHANCE_TO_BREAK_FROM_FIRE)
                    entity.GetComponent<ComponentBreakable>()?.Break();
            }
        }
    }
}