using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Framework.Input
{
    public class Pointing
    {
        private MouseState _oldMouseState;
        private MouseState _mouseState;

        public void Update()
        {
            _oldMouseState = _mouseState;
            _mouseState = Mouse.GetState();
        }

        public bool AreaDown(Rectangle area)
        {
            return area.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool WasAreaDown(Rectangle area)
        {
            return area.Contains(_oldMouseState.Position) && _oldMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool AreaUp(Rectangle area)
        {
            return !AreaDown(area);
        }

        public bool AreaClick(Rectangle area)
        {
            if (area.Contains(_mouseState.Position) &&
                _mouseState.LeftButton == ButtonState.Released &&
                _oldMouseState.LeftButton == ButtonState.Pressed) return true;

            return false;
        }

        public Point GetPosition()
        {
            return _mouseState.Position;
        }

        public Point GetDeltaPosition()
        {
            return _mouseState.Position - _oldMouseState.Position;
        }

        public bool AreaOver(Rectangle area)
        {
            return area.Contains(_mouseState.Position);
        }

        public bool WasOver(Rectangle area)
        {
            return area.Contains(_oldMouseState.Position);
        }
    }
}