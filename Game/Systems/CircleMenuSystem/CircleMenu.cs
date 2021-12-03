using System;
using Hevadea.Entities;
using Hevadea.Framework;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.CircleMenuSystem
{
    public enum CircleMenuState
    {
        Visible,
        FadingOut,
        FadingIn,
        Hidden,
    }

    public class CircleMenu : EntityComponent
    {
        private float _animation;

        public int SelectedItem { get; set; }
        public CircleMenuState State { get; set; } = CircleMenuState.Hidden;

        public float Animation
        {
            get => _animation;
            set => _animation = Mathf.Clamp01(value);
        }

        public float Opacity
        {
            get
            {
                switch (State)
                {
                    case CircleMenuState.FadingIn:
                        return Animation;
                    case CircleMenuState.FadingOut:
                        return 1f - Animation;
                    case CircleMenuState.Visible:
                        return 1f;
                    case CircleMenuState.Hidden:
                        return 0f;
                    default:
                        return 0f;
                }
            }
        }

        public void Shown()
        {
            if (State == CircleMenuState.FadingOut || State == CircleMenuState.Hidden)
            {
                State = CircleMenuState.FadingIn;
                Animation = 0f;
            }

            if (State == CircleMenuState.Visible) Animation = 0f;
        }

        public void Hide()
        {
            State = CircleMenuState.FadingOut;
            Animation = 0f;
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            if (Math.Abs(Animation - 1f) < 0.01f)
            {
                switch (State)
                {
                    case CircleMenuState.FadingIn:
                        State = CircleMenuState.Visible;
                        break;
                    case CircleMenuState.Visible:
                        State = CircleMenuState.FadingOut;
                        break;
                    case CircleMenuState.FadingOut:
                        State = CircleMenuState.Hidden;
                        break;
                }

                Animation = 0f;
            }

            Animation += (float) gameTime.ElapsedGameTime.TotalSeconds * (State == CircleMenuState.Visible ? 2f : 5);
        }
    }
}