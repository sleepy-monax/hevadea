using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace WorldOfImagination.GameComponent
{
    public class InputManager : GameComponent
    {
        private KeyboardState oldKeyState;
        private KeyboardState newKeyState;

        private MouseState oldMouseState;
        private MouseState newMouseState;
        public InputManager(WorldOfImaginationGame game) : base(game)
        {
        
        }

        public Point MousePosition => newMouseState.Position;
        
        public bool MouseLeft => newMouseState.LeftButton == ButtonState.Pressed;
        public bool MouseRight => newMouseState.RightButton == ButtonState.Pressed;
        public bool MouseMiddle => newMouseState.MiddleButton == ButtonState.Pressed;
        
        public bool MouseLeftClick => newMouseState.LeftButton == ButtonState.Released
                                   && oldMouseState.LeftButton == ButtonState.Pressed;
        public bool MouseRightClick => newMouseState.RightButton == ButtonState.Released
                                    && oldMouseState.RightButton == ButtonState.Pressed;
        public bool MouseMiddleClick => newMouseState.MiddleButton == ButtonState.Released
                                     && oldMouseState.MiddleButton == ButtonState.Pressed;


        public override void Initialize()
        {
            oldKeyState = newKeyState = Keyboard.GetState();
            oldMouseState = newMouseState = Mouse.GetState(Game.Window);
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            oldKeyState = newKeyState;
            oldMouseState = newMouseState;
            newKeyState = Keyboard.GetState();
            newMouseState = Mouse.GetState(Game.Window);
            
        }
    }
}