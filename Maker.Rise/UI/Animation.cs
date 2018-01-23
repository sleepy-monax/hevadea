using Maker.Utils;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Rise.UI
{
    public class FadingAnimation
    {
        public bool Show { get; set; } = false;
        public float Speed = 1f;
        private float debugSpeed = 1f;

        private float animation = 0f;


        public void Reset()
        {
            animation = 0f;
        }

        public float GetValue(EasingFunctions function)
        {
            return Easings.Interpolate(animation, function);
        }

        public void Update(GameTime gameTime)
        {
            if (Show)
            {
                animation = Math.Min(1f,
                    animation + (float) (gameTime.ElapsedGameTime.Milliseconds) / 100f * Speed * debugSpeed);
            }
            else
            {
                animation = Math.Max(0f,
                    animation - (float) (gameTime.ElapsedGameTime.Milliseconds) / 100f * Speed * debugSpeed);
            }
        }
    }
}