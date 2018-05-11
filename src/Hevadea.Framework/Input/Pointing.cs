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
        private TouchCollection _touchState;
        private TouchCollection _oldTouchState;
        private MouseState _oldMouseState;
        private MouseState _mouseState;

        public void Update()
        {
            _oldTouchState = _touchState;
            _touchState = TouchPanel.GetState();
            _oldMouseState = _mouseState;
            _mouseState = Mouse.GetState();
        }

        public void DrawDebug(SpriteBatch spritebatch)
        {
            foreach (var f in _oldTouchState)
            {
                spritebatch.PutPixel(f.Position, Color.Blue, 4);
            }

            foreach (var f in _touchState)
            {
                spritebatch.PutPixel(f.Position, Color.Red, 4);
            }
        }

        public bool AreaDown(Rectangle area)
        {
            foreach (var finger in _touchState)
            {
                if (area.Contains(finger.Position))
                {
                    return true;
                }
            }

            return area.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool WasAreaDown(Rectangle area)
        {
            foreach (var finger in _oldTouchState)
            {
                if (area.Contains(finger.Position))
                {
                    return true;
                }
            }

            return area.Contains(_oldMouseState.Position) && _oldMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool AreaUp(Rectangle area)
        {
            return !AreaDown(area);
        }

        public bool AreaClick(Rectangle area)
        {
            if (_touchState.Count > 0)
            {
                return _touchState.Where((t, i) =>
                    area.Contains(t.Position) &&
                    (t.State == TouchLocationState.Released &&
                     _oldTouchState[i].State == TouchLocationState.Moved)).Any();
            }

            if (area.Contains(_mouseState.Position) &&
                _mouseState.LeftButton == ButtonState.Released &&
                _oldMouseState.LeftButton == ButtonState.Pressed) return true;

            return false;
        }

        public List<Point> GetAreaOver(Rectangle area)
        {
            var result = new List<Point>();

            foreach (var finger in _touchState)
            {
                if (area.Contains(finger.Position))
                {
                    result.Add(finger.Position.ToPoint());
                }
            }

            if (area.Contains(_mouseState.Position))
            {
                result.Add(_mouseState.Position);
            }

            return result;
        }

        public bool AreaOver(Rectangle area)
        {
            foreach (var finger in _touchState)
            {
                if (area.Contains(finger.Position))
                {
                    return true;
                }
            }

            return area.Contains(_mouseState.Position);
        }

        public bool WasOver(Rectangle area)
        {
            foreach (var finger in _oldTouchState)
            {
                if (area.Contains(finger.Position))
                {
                    return true;
                }
            }

            return area.Contains(_oldMouseState.Position);
        }
    }
}