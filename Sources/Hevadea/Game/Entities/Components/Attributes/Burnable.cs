using Hevadea.Framework;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Interaction;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Attributes
{
    class Burnable : Light, IEntityComponentUpdatable
    {
        private float _dammages;
        private float _chanceToBreak;

        public float SpreadRange = 2f;
        private bool _isBurnning = false;
        private float _burnTimer;

        public bool IsBurnning
        {
            get => _isBurnning;
            set
            {
                if (value == true)
                {
                    _burnTimer = Rise.Rnd.NextFloat(7) + 3f;
                }

                _isBurnning = value;
            }
        }

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

        public void Update(GameTime gameTime)
        {
            IsBurnning = !(Owner.GetComponent<Swim>()?.IsSwiming ?? false) && IsBurnning;
            On = IsBurnning;

            if (_burnTimer < 0.01f)
            {
                IsBurnning = false;
            }

            if (IsBurnning)
            {
                _burnTimer -= gameTime.GetDeltaTime();

                Owner.GetComponent<Health>()?.Hurt(Owner, _dammages * gameTime.GetDeltaTime(), false);
                if (Rise.Rnd.NextDouble() < 0.1f)
                {
                    var Entities = Owner.Level.GetEntitiesOnArea(new Rectangle((int)(Owner.X - SpreadRange * 16), (int)(Owner.Y - SpreadRange * 16), (int)(SpreadRange * 16 * 2), (int)(SpreadRange * 16 * 2)));

                    foreach (var e in Entities)
                    {
                        if (e != Owner && Rise.Rnd.NextDouble() < 0.01f)
                        {
                            if (e.HasComponent<Burnable>())
                                e.GetComponent<Burnable>().IsBurnning = true;
                            e.GetComponent<Explode>()?.Do();
                        }
                    }
                }

                // Emit fire particles.
                Owner.Level.ParticleSystem.EmiteAt(
                    CreateFireParticle(),
                    Owner.X + Rise.Rnd.NextFloatRange(10),
                    Owner.Y + Rise.Rnd.NextFloatRange(10),
                    Rise.Rnd.NextFloatRange(4),
                    -Rise.Rnd.NextFloat(24));

                Owner.ParticleSystem.EmiteAt(
                    CreateFireParticle(),
                    Owner.X + Rise.Rnd.NextFloatRange(10),
                    Owner.Y + Rise.Rnd.NextFloatRange(10),
                    Rise.Rnd.NextFloatRange(4),
                    -Rise.Rnd.NextFloat(24));

                if (Rise.Rnd.NextDouble() < _chanceToBreak)
                    Owner.GetComponent<Breakable>()?.Break();
            }

        }
    }
}
