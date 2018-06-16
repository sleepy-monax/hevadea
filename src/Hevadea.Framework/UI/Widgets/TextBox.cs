using Hevadea.Framework.Utils;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Framework.Platform;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Framework.UI.Widgets
{
    public class TextBox : Widget
    {
        int _cursorIndex = 0;
        string _text = "";

        public Style  Style { get; set; } = Style.Focused;
        public Style FocusedStyle { get; set; } = Style.Focused;
        public string AcceptedChar { get; set; } = Charsets.ASCII;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                CursorIndex = Text.Length;
            }
        }

        public int CursorIndex
        {
            get => _cursorIndex;
            set => _cursorIndex = Mathf.Clamp(value, 0, Text.Length);
        }
        public TextBox()
        {
            CanGetFocus = true;
            Rise.Platform.TextInput += OnTextInput;
        }

        private void OnTextInput(object sender, PlatformTextInputEventArg e)
        {
            if (Focused)
            {
                if (AcceptedChar.Contains(e.Character))
                {
                    _text = _text.Insert(_cursorIndex, e.Character.ToString());
                    CursorIndex++;
                }
                else
                {
                    HandleSpecialChar(e.Character, e.Key);
                }
            }
        }

        private void HandleSpecialChar(char c, Keys key)
        {
            if (c == Charsets.Backspace && CursorIndex >= 1 && _text.Any())
            {
                _text = _text.Remove(_cursorIndex - 1, 1);
                CursorIndex--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Rise.Input.KeyPress(Keys.Left))
            {
                CursorIndex--;
            }

            if (Rise.Input.KeyPress(Keys.Right))
            {
                CursorIndex++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Style.Draw(spriteBatch, UnitBound);

            var textBound = Scale(Style.GetContent(UnitBound));
            var selectionBegin = Scale(Style.Font.MeasureString(Text.Substring(0, CursorIndex)).X);

            spriteBatch.DrawString(Style.Font, Text, textBound, Style.TextColor, Anchor.Left, Scale(1f));
            if (FocusedStyle) spriteBatch.DrawString(Style.Font, "_", new Rectangle(textBound.Location + new Point((int)selectionBegin, Scale(4)), textBound.Size), DrawText.Alignement.Left, DrawText.TextStyle.DropShadow, Style.Accent * Mathf.Sin((float)gameTime.TotalGameTime.TotalSeconds * Mathf.PI * Mathf.PI), Scale(1f));
        }
    }
}
