using Microsoft.Xna.Framework.Input;
using System;

namespace Hevadea.Framework.Platform
{
    public abstract class PlatformBase
    {
        public PlatformFamily Family { get; set; } = PlatformFamily.Desktop;

        public event EventHandler<PlatformTextInputEventArg> TextInput;

        public void RaiseTextInput(char c, Keys key)
        {
            TextInput?.Invoke(this, new PlatformTextInputEventArg(c, key));
        }

        public virtual void Stop()
        { }

        public abstract void Initialize();

        public abstract string GetPlatformName();

        public abstract int GetScreenWidth();

        public abstract int GetScreenHeight();

        public abstract string GetStorageFolder();

        public abstract void Update();

        public virtual float GetScreenScaling()
        {
            return 1;
        }
    }
}