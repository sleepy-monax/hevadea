using Maker.Utils;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise.UI
{
    public class EasingManager
    {
        public bool Show { get; set; } = false;
        public float Speed = 1f;
        private const float DebugSpeed = 1f;

        private float _value = 0f;
        private bool _ended;

        public AnimationEndedHandler AnimationEnded;
        public delegate void AnimationEndedHandler(EasingManager sender);

        public void Reset()
        {
            _value = 0f;
            _ended = false;
        }

        public float GetValue(EasingFunctions function)
        {
            return Easings.Interpolate(_value, function);
        }

        public void Update(GameTime gameTime)
        {
            if (Show)
            {
                _value = Math.Min(1f,
                    _value + (float)(gameTime.ElapsedGameTime.Milliseconds) / 100f * Speed * DebugSpeed);
            }
            else
            {
                _value = Math.Max(0f,
                    _value - (float)(gameTime.ElapsedGameTime.Milliseconds) / 100f * Speed * DebugSpeed);
            }

            if (_value == 1f && !_ended)
            {
                _ended = true;
                AnimationEnded?.Invoke(this);
            }
        }
    }
}
