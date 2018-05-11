using Hevadea.Framework.Graphic.Text;
using Hevadea.Framework.Input;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Hevadea.Framework.UI.Widgets.TextBox
{
    public class TextBox : Widget
    {
        private string clipboard;

        public TextWrapper  Text     { get; private set; }
        public TextRenderer Renderer { get; private set; }
        public Cursor       Cursor   { get; private set; }

        public event EventHandler<KeyboardInputManager.KeyEventArgs> EnterDown;

        public TextBox(int maxCharacters, string text,  SpriteFont spriteFont, Color cursorColor, Color selectionColor, int ticksPerToggle)
        {
            CanGetFocus = true;

            Text = new TextWrapper(maxCharacters)
            {
                String = text
            };

            Renderer = new TextRenderer(Text)
            {
                Font = spriteFont,
                Color = Color.White
            };

            Cursor = new Cursor(this, cursorColor, selectionColor, new Rectangle(0, 0, 1, 1), ticksPerToggle);

            Rise.Keyboard.CharPressed += CharacterTyped;
            Rise.Keyboard.KeyPressed += KeyPressed;
        }

        public void Clear()
        {
            Text.RemoveCharacters(0, Text.Length);
            Cursor.TextCursor = 0;
            Cursor.SelectedChar = null;
        }

        private void KeyPressed(object sender, KeyboardInputManager.KeyEventArgs e, KeyboardState ks)
        {
            if (IsFocus)
            {
                int oldPos = Cursor.TextCursor;
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        EnterDown?.Invoke(this, e);
                        break;
                    case Keys.Left:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            Cursor.TextCursor = IndexOfLastCharBeforeWhitespace(Cursor.TextCursor, Text.Characters);
                        }
                        else
                        {
                            Cursor.TextCursor--;
                        }
                        ShiftMod(oldPos);
                        break;
                    case Keys.Right:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            Cursor.TextCursor = IndexOfNextCharAfterWhitespace(Cursor.TextCursor, Text.Characters);
                        }
                        else
                        {
                            Cursor.TextCursor++;
                        }
                        ShiftMod(oldPos);
                        break;
                    case Keys.Home:
                        Cursor.TextCursor = 0;
                        ShiftMod(oldPos);
                        break;
                    case Keys.End:
                        Cursor.TextCursor = Text.Length;
                        ShiftMod(oldPos);
                        break;
                    case Keys.Delete:
                        if (DelSelection() == null && Cursor.TextCursor < Text.Length)
                        {
                            Text.RemoveCharacters(Cursor.TextCursor, Cursor.TextCursor + 1);
                        }
                        break;
                    case Keys.Back:
                        if (DelSelection() == null && Cursor.TextCursor > 0)
                        {
                            Text.RemoveCharacters(Cursor.TextCursor - 1, Cursor.TextCursor);
                            Cursor.TextCursor--;
                        }
                        break;
                    case Keys.A:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            if (Text.Length > 0)
                            {
                                Cursor.SelectedChar = 0;
                                Cursor.TextCursor = Text.Length;
                            }
                        }
                        break;
                    case Keys.C:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            clipboard = DelSelection(true);
                        }
                        break;
                    case Keys.X:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            if (Cursor.SelectedChar.HasValue)
                            {
                                clipboard = DelSelection();
                            }
                        }
                        break;
                    case Keys.V:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            if (clipboard != null)
                            {
                                DelSelection();
                                foreach (char c in clipboard)
                                {
                                    if (Text.Length < Text.MaxLength)
                                    {
                                        Text.InsertCharacter(Cursor.TextCursor, c);
                                        Cursor.TextCursor++;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void ShiftMod(int oldPos)
        {
            if (Rise.Keyboard.ShiftDown)
            {
                if (Cursor.SelectedChar == null)
                {
                    Cursor.SelectedChar = oldPos;
                }
            }
            else
            {
                Cursor.SelectedChar = null;
            }
        }

        private void CharacterTyped(object sender, KeyboardInputManager.CharacterEventArgs e, KeyboardState ks)
        {
            if (IsFocus && !Rise.Keyboard.CtrlDown)
            {
                if (Renderer.Font.IsLegalCharacter(e.Character) && !e.Character.Equals('\r') &&
                    !e.Character.Equals('\n'))
                {
                    DelSelection();
                    if (Text.Length < Text.MaxLength)
                    {
                        Text.InsertCharacter(Cursor.TextCursor, e.Character);
                        Cursor.TextCursor++;
                    }
                }
            }
        }

        private string DelSelection(bool fakeForCopy = false)
        {
            if (!Cursor.SelectedChar.HasValue)
            {
                return null;
            }
            int tc = Cursor.TextCursor;
            int sc = Cursor.SelectedChar.Value;
            int min = Math.Min(sc, tc);
            int max = Math.Max(sc, tc);
            string result = Text.String.Substring(min, max - min);

            if (!fakeForCopy)
            {
                Text.Replace(Math.Min(sc, tc), Math.Max(sc, tc), string.Empty);
                if (Cursor.SelectedChar.Value < Cursor.TextCursor)
                {
                    Cursor.TextCursor -= tc - sc;
                }
                Cursor.SelectedChar = null;
            }
            return result;
        }

        public static int IndexOfNextCharAfterWhitespace(int pos, char[] characters)
        {
            char[] chars = characters;
            char c = chars[pos];
            bool whiteSpaceFound = false;
            while (true)
            {
                if (c.Equals(' '))
                {
                    whiteSpaceFound = true;
                }
                else if (whiteSpaceFound)
                {
                    return pos;
                }

                ++pos;
                if (pos >= chars.Length)
                {
                    return chars.Length;
                }
                c = chars[pos];
            }
        }

        public static int IndexOfLastCharBeforeWhitespace(int pos, char[] characters)
        {
            char[] chars = characters;

            bool charFound = false;
            while (true)
            {
                --pos;
                if (pos <= 0)
                {
                    return 0;
                }
                var c = chars[pos];

                if (c.Equals(' '))
                {
                    if (charFound)
                    {
                        return ++pos;
                    }
                }
                else
                {
                    charFound = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Renderer.Destination = Host;
            Renderer.Update();
            Cursor.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Renderer.Draw(spriteBatch);

            if (IsFocus)
            {
                Cursor.Draw(spriteBatch);
            }
        }
    }
}