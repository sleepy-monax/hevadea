using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Hevadea.Framework.Input
{
    public class KeyboardInputManager
    {
        public class CharacterEventArgs : EventArgs
        {
            public char Character { get; private set; }

            public CharacterEventArgs(char character)
            {
                Character = character;
            }
        }

        public class KeyEventArgs : EventArgs
        {
            public Keys KeyCode { get; private set; }

            public KeyEventArgs(Keys keyCode)
            {
                KeyCode = keyCode;
            }
        }

        public delegate void CharEnteredHandler(object sender, CharacterEventArgs e, KeyboardState ks);

        public delegate void KeyEventHandler(object sender, KeyEventArgs e, KeyboardState ks);

        public readonly char[] SPECIAL_CHARACTERS = { '\a', '\b', '\n', '\r', '\f', '\t', '\v' };

        private Game game;

        public event CharEnteredHandler CharPressed;

        public event KeyEventHandler KeyPressed;

        public event KeyEventHandler KeyDown;

        public event KeyEventHandler KeyUp;

        private KeyboardState prevKeyState;

        private Keys? repChar;
        private DateTime downSince = DateTime.Now;
        private float timeUntilRepInMillis;
        private int repsPerSec;
        private DateTime lastRep = DateTime.Now;
        private bool filterSpecialCharacters;

        public void Initialize(Game g, float timeUntilRepInMilliseconds, int repsPerSecond, bool filterSpecialCharactersFromCharPressed = true)
        {
            game = g;
            timeUntilRepInMillis = timeUntilRepInMilliseconds;
            repsPerSec = repsPerSecond;
            filterSpecialCharacters = filterSpecialCharactersFromCharPressed;
            Rise.Platform.TextInput += Platform_TextInput;
        }

        private void Platform_TextInput(object sender, Platform.PlatformTextInputEventArg e)
        {
            if (CharPressed != null)
            {
                if (!filterSpecialCharacters || !SPECIAL_CHARACTERS.Contains(e.Character))
                {
                    CharPressed(null, new CharacterEventArgs(e.Character), Keyboard.GetState());
                }
            }
        }

        public bool ShiftDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift);
            }
        }

        public bool CtrlDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.RightControl);
            }
        }

        public bool AltDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftAlt) || state.IsKeyDown(Keys.RightAlt);
            }
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach (Keys key in (Keys[])Enum.GetValues(typeof(Keys)))
            {
                if (JustPressed(keyState, key))
                {
                    KeyDown?.Invoke(null, new KeyEventArgs(key), keyState);
                    if (KeyPressed != null)
                    {
                        downSince = DateTime.Now;
                        repChar = key;
                        KeyPressed(null, new KeyEventArgs(key), keyState);
                    }
                }
                else if (JustReleased(keyState, key))
                {
                    if (KeyUp != null)
                    {
                        if (repChar == key)
                        {
                            repChar = null;
                        }
                        KeyUp(null, new KeyEventArgs(key), keyState);
                    }
                }

                if (repChar != null && repChar == key && keyState.IsKeyDown(key))
                {
                    DateTime now = DateTime.Now;
                    TimeSpan downFor = now.Subtract(downSince);
                    if (downFor.CompareTo(TimeSpan.FromMilliseconds(timeUntilRepInMillis)) > 0)
                    {
                        // Should repeat since the wait time is over now.
                        TimeSpan repeatSince = now.Subtract(lastRep);
                        if (repsPerSec != 0 && repeatSince.CompareTo(TimeSpan.FromMilliseconds(1000f / repsPerSec)) > 0)
                        {
                            // Time for another key-stroke.
                            if (KeyPressed != null)
                            {
                                lastRep = now;
                                KeyPressed(null, new KeyEventArgs(key), keyState);
                            }
                        }
                    }
                }
            }

            prevKeyState = keyState;
        }

        private bool JustPressed(KeyboardState keyState, Keys key)
        {
            return keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key);
        }

        private bool JustReleased(KeyboardState keyState, Keys key)
        {
            return prevKeyState.IsKeyDown(key) && keyState.IsKeyUp(key);
        }

        public void Dispose()
        {
            CharPressed = null;
            KeyDown = null;
            KeyPressed = null;
            KeyUp = null;
        }
    }
}