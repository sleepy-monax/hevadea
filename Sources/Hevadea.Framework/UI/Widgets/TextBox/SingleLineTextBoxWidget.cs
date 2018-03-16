using System;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.Text;
using Hevadea.Framework.Input;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Framework.UI.Widgets.TextBox
{
    public class SingleLineTextBoxWidget : Widget
    {
        private string clipboard;
        private int? _selectedChar;
        private int _textCursor;

        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; } = Color.LightGray;

        public int TextCursor
        {
            get { return _textCursor; }
            set { _textCursor = value.Clamp(0, Text.Length); }
        }
        public int? SelectedChar
        {
            get { return _selectedChar; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value != TextCursor)
                    {
                        _selectedChar = (short)(value.Value.Clamp(0, Text.Length));
                    }
                }
                else
                {
                    _selectedChar = null;
                }
            }
        }

        public TextWrapper Text { get; private set; }
        public event EventHandler<KeyboardInputManager.KeyEventArgs> EnterDown;

        public SingleLineTextBoxWidget(int maxCharacters, string text, SpriteFont spriteFont)
        {
            CanGetFocus = true;

            Text = new TextWrapper(maxCharacters)
            {
                String = text
            };

            Font = spriteFont;

            Rise.Keyboard.CharPressed += CharacterTyped;
            Rise.Keyboard.KeyPressed += KeyPressed;
        }

        public void Clear()
        {
            Text.RemoveCharacters(0, Text.Length);
            TextCursor = 0;
        }

        private void KeyPressed(object sender, KeyboardInputManager.KeyEventArgs e, KeyboardState ks)
        {
            if (IsFocus)
            {
                int oldPos = TextCursor;
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        EnterDown?.Invoke(this, e);
                        break;
                    case Keys.Left:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            TextCursor = IndexOfLastCharBeforeWhitespace(TextCursor, Text.Characters);
                        }
                        else
                        {
                            TextCursor--;
                        }
                        ShiftMod(oldPos);
                        break;
                    case Keys.Right:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            TextCursor = IndexOfNextCharAfterWhitespace(TextCursor, Text.Characters);
                        }
                        else
                        {
                            TextCursor++;
                        }
                        ShiftMod(oldPos);
                        break;
                    case Keys.Home:
                        TextCursor = 0;
                        ShiftMod(oldPos);
                        break;
                    case Keys.End:
                        TextCursor = Text.Length;
                        ShiftMod(oldPos);
                        break;
                    case Keys.Delete:
                        if (DelSelection() == null && TextCursor < Text.Length)
                        {
                            Text.RemoveCharacters(TextCursor, TextCursor + 1);
                        }
                        break;
                    case Keys.Back:
                        if (DelSelection() == null && TextCursor > 0)
                        {
                            Text.RemoveCharacters(TextCursor - 1, TextCursor);
                            TextCursor--;
                        }
                        break;
                    case Keys.A:
                        if (Rise.Keyboard.CtrlDown)
                        {
                            if (Text.Length > 0)
                            {
                                SelectedChar = 0;
                                TextCursor = Text.Length;
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
                            if (SelectedChar.HasValue)
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
                                        Text.InsertCharacter(TextCursor, c);
                                        TextCursor++;
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
                if (SelectedChar == null)
                {
                    SelectedChar = oldPos;
                }
            }
            else
            {
                SelectedChar = null;
            }
        }

        private void CharacterTyped(object sender, KeyboardInputManager.CharacterEventArgs e, KeyboardState ks)
        {
            if (IsFocus && !Rise.Keyboard.CtrlDown)
            {
                // if (Renderer.Font.IsLegalCharacter(e.Character) && !e.Character.Equals('\r') &&
                if (Font.IsLegalCharacter(e.Character) && !e.Character.Equals('\r') &&
                    !e.Character.Equals('\n'))
                {
                    DelSelection();
                    if (Text.Length < Text.MaxLength)
                    {
                        Text.InsertCharacter(TextCursor, e.Character);
                        TextCursor++;
                    }
                }
            }
        }

        private string DelSelection(bool fakeForCopy = false)
        {
            if (!SelectedChar.HasValue)
            {
                return null;
            }
            int tc = TextCursor;
            int sc = SelectedChar.Value;
            int min = Math.Min(sc, tc);
            int max = Math.Max(sc, tc);
            string result = Text.String.Substring(min, max - min);

            if (!fakeForCopy)
            {
                Text.Replace(Math.Min(sc, tc), Math.Max(sc, tc), string.Empty);
                if (SelectedChar.Value < TextCursor)
                {
                    TextCursor -= tc - sc;
                }
                SelectedChar = null;
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
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color.White * 0.1f);

            if (IsFocus)
            {
                var curx = Text.MeasureCharacterWidths(TextCursor, Font);
                var pos = Host.Center.Y - Font.LineSpacing / 2;



                if (_selectedChar.HasValue)
                {
                    var selx = Text.MeasureCharacterWidths(SelectedChar ?? 0, Font);
                    if (selx > curx)
                    {
                        spriteBatch.DrawRectangle(new Rectangle(Host.X + curx, pos, selx - curx, Font.LineSpacing), Color.Gold * 0.5f);
                        spriteBatch.FillRectangle(new Rectangle(Host.X + curx, pos, selx - curx, Font.LineSpacing), Color.Gold * 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawRectangle(new Rectangle(Host.X + selx, pos, curx - selx, Font.LineSpacing), Color.Gold * 0.5f);
                        spriteBatch.FillRectangle(new Rectangle(Host.X + selx, pos, curx - selx, Font.LineSpacing), Color.Gold * 0.5f);
                    }
                }
                else
                {
                    spriteBatch.FillRectangle(new Rectangle(Host.X + curx, pos, 1, Font.LineSpacing), Color.Gold);
                }

            }

            spriteBatch.DrawString(Font, Text.String, Host, DrawText.Alignement.Left, DrawText.TextStyle.DropShadow, TextColor);
        }
    }
}