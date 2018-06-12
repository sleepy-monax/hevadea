using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Hevadea.Framework.Input
{
    public class LegacyInputManager
    {
        KeyboardState _oldKeyboardState;
        KeyboardState _newKeyboardState;

        public bool AnyKeyDown()
        {
            return ((Keys[])Enum.GetValues(typeof(Keys))).Where((k)=>_newKeyboardState.IsKeyDown(k)).Any();
        }

        public bool KeyDown(Keys key)
        {
            return _newKeyboardState.IsKeyDown(key);
        }

        public bool KeyPress(Keys key)
        {
            return _newKeyboardState.IsKeyUp(key) && _oldKeyboardState.IsKeyDown(key);
        }

        public void Initialize()
        {
            _oldKeyboardState = _newKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            _oldKeyboardState = _newKeyboardState;
            _newKeyboardState = Keyboard.GetState();
        }
    }
}