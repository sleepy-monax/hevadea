using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Hevadea.Framework.Input
{
    public class LegacyInputManager
    {
        private KeyboardState _oldKeyboardState;
        private KeyboardState _newKeyboardState;

        public void Initialize()
        {
            _oldKeyboardState = _newKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            _oldKeyboardState = _newKeyboardState;
            _newKeyboardState = Keyboard.GetState();
        }

        public bool AnyKeyDown()
        {
            return _newKeyboardState.GetPressedKeys().Any();
        }

        public bool KeyUp(Keys key)
        {
            return _newKeyboardState.IsKeyUp(key);
        }

        public bool KeyDown(Keys key)
        {
            return _newKeyboardState.IsKeyDown(key);
        }

        public bool KeyTyped(Keys key)
        {
            return _newKeyboardState.IsKeyUp(key) && _oldKeyboardState.IsKeyDown(key);
        }
    }
}