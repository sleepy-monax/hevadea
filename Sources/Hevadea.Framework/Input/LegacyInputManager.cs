using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Hevadea.Framework.Input
{
    public class LegacyInputManager
    {
        private KeyboardState oldKeyState;
        private KeyboardState newKeyState;

        private MouseState _oldMouseState;
        private MouseState _mouseState;

        private TouchCollection _oldToucheState;
        private TouchCollection _newToucheState;

        public MouseState GetMouseState()
        {
            return _mouseState;
        }

        public MouseState GetOldMouseState()
        {
            return _oldMouseState;
        }

        public Point MousePosition => GetMousePosition();

        private Point GetMousePosition()
        {
            if (_newToucheState.Count > 0)
            {
                return _newToucheState[0].Position.ToPoint();
            }

            return _mouseState.Position;
        }

        public bool MouseLeft => _mouseState.LeftButton == ButtonState.Pressed;
        public bool MouseRight => _mouseState.RightButton == ButtonState.Pressed;
        public bool MouseMiddle => _mouseState.MiddleButton == ButtonState.Pressed;

        public bool MouseLeftClick => GetLeftClick();
        private bool GetLeftClick()
        {
            return _mouseState.LeftButton == ButtonState.Released
                                      && _oldMouseState.LeftButton == ButtonState.Pressed;
        }
        public bool MouseRightClick => _mouseState.RightButton == ButtonState.Released
                                       && _oldMouseState.RightButton == ButtonState.Pressed;

        public bool MouseMiddleClick => _mouseState.MiddleButton == ButtonState.Released
                                        && _oldMouseState.MiddleButton == ButtonState.Pressed;

        public bool MouseScrollDown => _mouseState.ScrollWheelValue < _oldMouseState.ScrollWheelValue;
        public bool MouseScrollUp => _mouseState.ScrollWheelValue > _oldMouseState.ScrollWheelValue;

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
            _oldMouseState = _mouseState = Mouse.GetState();
        }
        public void Update(GameTime gameTime)
        {
            _oldToucheState = _newToucheState;
            _newToucheState = TouchPanel.GetState();
            oldKeyState = newKeyState;
            _oldMouseState = _mouseState;
            newKeyState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }
    }
}