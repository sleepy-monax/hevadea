using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Platform
{
    public class PlatformTextInputEventArg : EventArgs
    {
        public char Character { get; }
        public Keys Key { get; }

        public PlatformTextInputEventArg(char character, Keys key = Keys.None)
        {
            Character = character;
            Key = Key;
        }
    }
}
