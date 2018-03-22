using Hevadea.Framework;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Burnable:Light
    {
        private float _dammages;
        private float _chanceToBreak;

        public float SpreadRange = 2f;
        public bool IsBurnning { get; set; } = false;

        public Burnable(float dammages, float chanceToBreak = 0.005f)
        {
            _dammages = dammages;
            IsBurnning = false;
            _chanceToBreak = chanceToBreak;
        }

        public void SetInFire()
        {
            IsBurnning = true;
        }

        private Particle CreateFireParticle()
        {
            return new Color2Particle
            {
                Color = Rise.Rnd.NextValue(Color.Orange, Color.Yellow, Color.Gold, Color.White * 0.5f),
                FadingColor = Color.Red * 0.5f,
                Life = Rise.Rnd.NextFloat(3),
                Size = Rise.Rnd.NextFloat(8)
            };
        }

        public override void Update(GameTime gameTime)
        {
            IsBurnning = !(Entity.GetComponent<Swim>()?.IsSwiming ?? false) && IsBurnning;
            
            On = IsBurnning;
            if (!IsBurnning) return;
            
            Entity.GetComponent<Health>()?.Hurt(Entity, _dammages * gameTime.GetDeltaTime(), false);
            if (Rise.Rnd.NextDouble() < 0.1f)
            {
                var entities = Entity.Level.GetEntitiesOnArea(new Rectangle((int)(Entity.X - SpreadRange*16), (int)(Entity.Y - SpreadRange*16), (int)(SpreadRange*16 * 2),(int)(SpreadRange*16 * 2)));

                foreach (var e in entities)
                {
                    if (Rise.Rnd.NextDouble() < 0.01f)
                    {
                        e.GetComponent<Burnable>()?.SetInFire();
                        e.GetComponent<Explode>()?.Do();
                    }
                }
            }

            // Emit fire particles.
            Entity.Level.ParticleSystem.EmiteAt(
                CreateFireParticle(),
                Entity.X + Rise.Rnd.NextFloatRange(10),
                Entity.Y + Rise.Rnd.NextFloatRange(10),
                Rise.Rnd.NextFloatRange(4),
                -Rise.Rnd.NextFloat(24));

            Entity.ParticleSystem.EmiteAt(
                CreateFireParticle(),
                Entity.X + Rise.Rnd.NextFloatRange(10),
                Entity.Y + Rise.Rnd.NextFloatRange(10),
                Rise.Rnd.NextFloatRange(4),
                -Rise.Rnd.NextFloat(24));

            if (Rise.Rnd.NextDouble() < _chanceToBreak)
                Entity.GetComponent<Breakable>()?.Break();
        }
    }
}
