using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

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
                    (t.State == TouchLocationState.Moved &&
                     _oldTouchState[i].State == TouchLocationState.Pressed)).Any();
            }

            if (area.Contains(_mouseState.Position) && 
                _mouseState.LeftButton == ButtonState.Released &&
                _oldMouseState.LeftButton == ButtonState.Pressed) return true;
            
            return false;
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
    }
}