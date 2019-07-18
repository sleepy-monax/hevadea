using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic.Particles
{
    public class Particle
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float AccelerationX { get; set; } = 0f;
        public float AccelerationY { get; set; } = 0f;

        public float Size { get; set; } = 2f;
        public float Life { get; set; } = 3f;
        public float FadeOut { get; set; } = 3f;
        public float FadeOutAnimation => Mathf.Min(1f, Life / FadeOut);
        public EasingFunctions FadeOutEasing { get; set; } = EasingFunctions.Linear;

        public bool IsAffectedByGravity { get; set; } = false;

        public Particle SetAcceleration(float ax, float ay)
        {
            AccelerationX = ax;
            AccelerationY = ay;

            return this;
        }

        public Particle SetPosition(float x, float y)
        {
            X = x;
            Y = y;

            return this;
        }

        public void Update(ParticleSystem particleSystem, GameTime gameTime)
        {
            X += AccelerationX * (float) gameTime.ElapsedGameTime.TotalSeconds;
            Y += AccelerationY * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (IsAffectedByGravity)
            {
                AccelerationX += particleSystem.Gravity.X * (float) gameTime.ElapsedGameTime.TotalSeconds;
                AccelerationY += particleSystem.Gravity.Y * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = Size * Easing.Interpolate(FadeOutAnimation, FadeOutEasing);
            spriteBatch.PutPixel(new Vector2(X, Y) - new Vector2(s, s) / 2, Color.White, s);
        }
    }
}