using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Rise.UI
{
    public class Animation
    {
        public bool Show { get; set; } = false;
        public float Speed = 1f;
        private float debugSpeed = 1f;

        private float animation = 0f;
        private float animation_final = 0f;

        public float TwoPhases => (animation * 0.70f + animation_final * 0.30f);
        public float Linear => animation;
        public float SinTwoPhases => MathUtils.Interpolate(animation * 0.70f + animation_final * 0.30f);
        public float SinLinear => MathUtils.Interpolate(animation);

        public void Reset()
        {
            animation = 0f;
            animation_final = 0f;
        }

        public void Update(GameTime gameTime)
        {
            if (Show)
            {
                animation = Math.Min(1f,
                    animation + (float) (gameTime.ElapsedGameTime.Milliseconds) / 100f * Speed * debugSpeed);
                animation_final = Math.Min(1f,
                    animation_final + (float) (gameTime.ElapsedGameTime.Milliseconds) / 400f * Speed * debugSpeed);
            }
            else
            {
                animation = Math.Max(0f,
                    animation - (float) (gameTime.ElapsedGameTime.Milliseconds) / 100f * Speed * debugSpeed);
                animation_final = Math.Min(0f,
                    animation_final + (float) (gameTime.ElapsedGameTime.Milliseconds) / 400f * Speed * debugSpeed);
            }
        }
    }
}