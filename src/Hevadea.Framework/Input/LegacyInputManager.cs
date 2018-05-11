using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Framework.Input
{
    public class LegacyInputManager
    {
        private KeyboardState oldKeyState;
        private KeyboardState newKeyState;

        public bool KeyDown(Keys key)
        {
            return newKeyState.IsKeyDown(key);
        }

        public bool KeyPress(Keys key)
        {
            return newKeyState.IsKeyUp(key) && oldKeyState.IsKeyDown(key);
        }

        public void Initialize()
        {
            oldKeyState = newKeyState = Keyboard.GetState();
        }
        public void Update(GameTime gameTime)
        {
            oldKeyState = newKeyState;
            newKeyState = Keyboard.GetState();
        }
    }
}