using Hevadea.Entities;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic.Particles;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.ElementalSystem
{
    public class FireParticles : GameSystem, IEntityProcessSystem
    {
        public FireParticles()
        {
            Filter = new Filter().AllOf(typeof(Flammable));
        }

        public static Particle CreateFireParticle()
        {
            return new Color2Particle
            {
                Color = Rise.Rnd.NextValue(Color.Orange, Color.Yellow, Color.Gold, Color.White * 0.5f),
                FadingColor = Color.Red * 0.5f,
                Life = Rise.Rnd.NextFloat(3),
                Size = Rise.Rnd.NextFloat(8)
            };
        }

        public void Process(Entity entity, GameTime gameTime)
        {
            var fire = entity.GetComponent<Flammable>();

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
                for (int i = 0; i < 16; i++)
                {
                    entity.Level.ParticleSystem.EmiteAt(
                        new ColoredParticle { Color = Color.Black * 0.25f},
                        entity.X + Rise.Rnd.NextFloatRange(5),
                        entity.Y + Rise.Rnd.NextFloatRange(5),
                        Rise.Rnd.NextFloatRange(4),
                        -Rise.Rnd.NextFloat(8));

                    entity.ParticleSystem.EmiteAt(
                        new ColoredParticle { Color = Color.Black * 0.25f},
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
