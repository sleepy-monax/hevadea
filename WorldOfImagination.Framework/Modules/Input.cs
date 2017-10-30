using OpenTK;
using OpenTK.Input;
using WorldOfImagination.Framework;

namespace WorldOfImagination.Framework.Modules
{
    public class Input : IUpdateable
    {        
        public Input()
        {

        }

        private KeyboardState OldKeyboardState;
        private KeyboardState CurrentKeyboardState;

        private MouseState OldMouseState;
        private MouseState CurrentMouseState;
        
        public void Update(float deltaTime)
        {
            OldKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            OldMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public bool IsKeyboardKeyDown(Key key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyboardKeyPress(Key key)
        {
             return CurrentKeyboardState.IsKeyUp(key) && OldKeyboardState.IsKeyDown(key);
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return CurrentMouseState.IsButtonDown(button);
        }
        public bool IsMouseButtonClick(MouseButton button)
        {
             return CurrentMouseState.IsButtonUp(button) && OldMouseState.IsButtonDown(button);
        }

        public Vector2 GetMousePosition()
        {
            return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public Vector2 GetMouseVector()
        {
            return new Vector2(OldMouseState.X - CurrentMouseState.X, OldMouseState.Y - CurrentMouseState.Y);
        }
    }
}