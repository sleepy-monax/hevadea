using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic.Particles;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.ElementalSystem
{
    public class FireParticles : EntityUpdateSystem
    {
        public FireParticles()
        {
            Filter.AllOf(typeof(ComponentFlammable));
        }

        public static Particle CreateFireParticle()
        {
            return new Color2Particle
            {
                Color = Rise.Rnd.Pick(Color.Orange, Color.Yellow, Color.Gold, Color.White * 0.5f),
                FadingColor = Color.Red * 0.5f,
                Life = Rise.Rnd.NextFloat(3),
                Size = Rise.Rnd.NextFloat(8)
            };
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var fire = entity.GetComponent<ComponentFlammable>();

            if (fire.IsBurning)
            {
                // Emit some particles
                entity.Level.ParticleSystem.EmiteAt(
                    CreateFireParticle(),
                    entity.X + Rise.Rnd.NextFloatRange(10),
                    entity.Y + Rise.Rnd.NextFloatRange(10),
                    Rise.Rnd.NextFloatRange(4),
                    -Rise.Rnd.NextFloat(24));

                entity.ParticleSystem.EmiteAt(
                    CreateFireParticle(),
                    entity.X + Rise.Rnd.NextFloatRange(10),
                    entity.Y + Rise.Rnd.NextFloatRange(10),
                    Rise.Rnd.NextFloatRange(4),
                    -Rise.Rnd.NextFloat(24));
            }

            if (fire.GotExtinguish)
            {
                // do a puff of smoke if the fire got extinguish.
                for (var i = 0; i < 16; i++)
                {
                    entity.Level.ParticleSystem.EmiteAt(
                        new ColoredParticle {Color = Color.Black * 0.25f},
                        entity.X + Rise.Rnd.NextFloatRange(5),
                        entity.Y + Rise.Rnd.NextFloatRange(5),
                        Rise.Rnd.NextFloatRange(4),
                        -Rise.Rnd.NextFloat(8));

                    entity.ParticleSystem.EmiteAt(
                        new ColoredParticle {Color = Color.Black * 0.25f},
                        entity.X + Rise.Rnd.NextFloatRange(5),
                        entity.Y + Rise.Rnd.NextFloatRange(5),
                        Rise.Rnd.NextFloatRange(4),
                        -Rise.Rnd.NextFloat(8));
                }

                fire.GotExtinguish = false;
            }
        }
    }
}