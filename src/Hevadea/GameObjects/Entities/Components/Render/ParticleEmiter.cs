using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.GameObjects.Entities.Components.Render
{
    public class Particle
    {
        public Color Color { get; set; }
        public float Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Momentum { get; set; }
        public double LifeTime { get; set; }

        public Particle(Vector2 position, Vector2 momentum, double lifeTime = 1, float size = 1,
            Color color = default(Color))
        {
            Color = color;
            Size = size;
            Position = position;
            Momentum = momentum;
            LifeTime = lifeTime;
        }
    }

    public class ParticleEmiter : EntityComponent, IEntityComponentDrawable, IEntityComponentUpdatable
    {
        private readonly List<Particle> _particles;

        public ParticleEmiter()
        {
            _particles = new List<Particle>();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var p in _particles) spriteBatch.PutPixel(p.Position, p.Color, p.Size);
        }

        public void Update(GameTime gameTime)
        {
            // No particle... nothing to do here.
            if (!_particles.Any()) return;

            var toRemove = new List<Particle>();

            foreach (var p in _particles)
            {
                p.Position = p.Position + p.Momentum * (float)gameTime.ElapsedGameTime.TotalSeconds;
                p.LifeTime -= gameTime.ElapsedGameTime.TotalSeconds;

                if (p.LifeTime < 0) toRemove.Add(p);
            }

            foreach (var p in toRemove) _particles.Remove(p);
        }

        public void EmitteParticle(Vector2 position, Vector2 momentum, float lifeTime, float size, Color color)
        {
            _particles.Add(new Particle(position, momentum, lifeTime, size, color));
        }
    }
}