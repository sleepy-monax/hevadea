using System;
using Microsoft.Xna.Framework.Input;

namespace Maker.Rise.UI
{
    public class MouseEventArgs : EventArgs
    {
        public MouseState State { get; }

        public MouseEventArgs(MouseState state)
        {
            State = state;
        }
    }
}