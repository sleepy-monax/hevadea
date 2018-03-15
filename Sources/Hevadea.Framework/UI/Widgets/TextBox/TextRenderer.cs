using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets.TextBox
{
    public class TextRenderer
    {
        private readonly Text _text;
        private RenderTarget2D _renderTarget;
        private SpriteBatch _sb;
        private Texture2D _cache; // Cached texture that has all of the characters.
        
        public Rectangle Destination { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; } = Color.White;

        internal readonly short[] X;
        internal readonly short[] Y;

        // With of the character.
        internal readonly byte[] Width;
        // Row the character is on.
        private readonly byte[] row;

        public void Dispose()
        {
            _cache?.Dispose();
            _cache = null;
            _renderTarget?.Dispose();
            _renderTarget = null;
            Font = null;
            _sb?.Dispose();
            _sb = null;
        }

        public TextRenderer(Text text)
        {
            _text = text;

            X = new short[_text.MaxLength];
            Y = new short[_text.MaxLength];
            Width = new byte[_text.MaxLength];

            row = new byte[_text.MaxLength];
        }

        public void Update()
        {
            if (!_text.IsDirty)
            {
                return;
            }

            MeasureCharacterWidths();
            _cache = RenderText();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_cache != null)
            {
                spriteBatch.Draw(_cache, Destination, Color.White);
            }
        }

        public int CharAt(Point localLocation)
        {
            Rectangle charRectangle = new Rectangle(0, 0, 0, Font.LineSpacing);

            int r = localLocation.Y / (Font.LineSpacing);

            for (short i = 0; i < _text.Length; i++)
            {
                if (row[i] != r)
                {
                    continue;
                }

                // Rectangle that encompasses the current character.
                charRectangle.X = X[i];
                charRectangle.Y = Y[i];
                charRectangle.Width = Width[i];

                // Click on a character so put the cursor in front of it.
                if (charRectangle.Contains(localLocation))
                {
                    return i;
                }

                // Next character is not on the correct row so this is the last character for this row so select it.
                if (i < _text.Length - 1 && row[i + 1] != r)
                {
                    return i;
                }
            }

            // Missed a character so return the end.
            return _text.Length;
        }

        private void MeasureCharacterWidths()
        {
            for (int i = 0; i < _text.Length; i++)
            {
                Width[i] = MeasureCharacter(i);
            }
        }

        private byte MeasureCharacter(int location)
        {
            string value = _text.String;
            float front = Font.MeasureString(value.Substring(0, location)).X;
            float end = Font.MeasureString(value.Substring(0, location + 1)).X;

            return (byte) (end - front);
        }

        private Texture2D RenderText()
        {
            if (_sb == null)
            {
                _sb = Rise.Graphic.CreateSpriteBatch();
            }
            if (_renderTarget == null)
            {
                _renderTarget = Rise.Graphic.CreateRenderTarget(Destination.Width, Destination.Height);
            }

            Rise.Graphic.SetRenderTarget(_renderTarget);
            Rise.Graphic.Clear(Color.Transparent);

            int start = 0;
            float height = 0.0f;

            _sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            while (true)
            {
                start = RenderLine(_sb, start, height);

                if (start >= _text.Length)
                {
                    _sb.End();
                    Rise.Graphic.SetDefaultRenderTarget();

                    return _renderTarget;
                }

                height += Font.LineSpacing;
            }
        }

        private int RenderLine(SpriteBatch spriteBatch, int start, float height)
        {
            int breakLocation = start;
            float lineLength = 0.0f;
            byte r = (byte) (height / Font.LineSpacing);

            string t = _text.String;
            string tempText;

            // Starting from end of last line loop though the characters.
            for (int iCount = start; iCount < _text.Length; iCount++)
            {
                // Calculate screen location of current character.
                X[iCount] = (short) lineLength;
                Y[iCount] = (short) height;
                row[iCount] = r;

                // Calculate the width of the current line.
                lineLength += Width[iCount];

                // Current line is too long need to split it.
                if (lineLength > Destination.Width)
                {
                    if (breakLocation == start)
                    {
                        // Have to split a word.
                        // Render line and return start of new line.
                        tempText = t.Substring(start, iCount - start);
                        spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), Color);
                        return iCount + 1;
                    }

                    // Have a character we can split on.
                    // Render line and return start of new line.
                    tempText = t.Substring(start, breakLocation - start);
                    spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), Color);
                    return breakLocation + 1;
                }

                // Handle characters that force/allow for breaks.
                switch (_text.Characters[iCount])
                {
                    // These characters force a line break.
                    case '\r':
                    case '\n':
                        //Render line and return start of new line.
                        tempText = t.Substring(start, iCount - start);
                        spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), Color);
                        return iCount + 1;
                    // These characters are good break locations.
                    case '-':
                    case ' ':
                        breakLocation = iCount + 1;
                        break;
                }
            }

            // We hit the end of the text box render line and return
            // _textData.Length so RenderText knows to return.
            tempText = t.Substring(start, _text.Length - start);
            spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), Color);
            return _text.Length;
        }
}
}