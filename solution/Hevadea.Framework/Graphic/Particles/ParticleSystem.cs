using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Framework.Graphic.Particles
{
    public class ParticleSystem
    {
        public Vector2 Gravity { get; set; } = new Vector2(0, 1000);

        private readonly List<Particle> _particles = new List<Particle>();
        private int _counter = 0;

        public void EmiteAt(Particle particle, float x, float y, float ax, float ay)
        {
            _particles.Add(particle.SetPosition(x, y).SetAcceleration(ax, ay));
            _counter++;
        }

        public void EmiteAtAngle(Particle particle, float x, float y, float angle, float acceleration)
        {
            var vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * acceleration;
            EmiteAt(particle, x, y, vec.X, vec.Y);
        }

        public void Clear()
        {
            _particles.Clear();
            _counter = 0;
        }

        public int Count()
        {
            return _counter;
        }

        public void Update(GameTime gameTime)
        {
            var particles = _particles.Clone();

            foreach (var p in particles)
            {
                p.Update(this, gameTime);
                p.Life -= gameTime.GetDeltaTime();

                if (p.Life <= 0f)
                {
                    _particles.Remove(p);
                    _counter--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var p in _particles) p.Draw(spriteBatch, gameTime);
        }
    }
}