using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets.TextBox
{
    public class Cursor
    {
        public Color Color { get; set; }
        public Color Selection { get; set; }
        public Rectangle Icon { get; set; }

        public bool Active { get; set; }

        private bool visible;
        private readonly int ticksPerBlink;
        private int ticks;

        /// <summary>
        ///     The current location of the cursor in the array
        /// </summary>
        public int TextCursor
        {
            get { return textCursor; }
            set { textCursor = value.Clamp(0, textBox.Text.Length); }
        }

        /// <summary>
        ///     All characters between SelectedChar and the TextCursor are selected
        ///     when SelectedChar != null. Cannot be the same as the TextCursor value.
        /// </summary>
        public int? SelectedChar
        {
            get { return selectedChar; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value != TextCursor)
                    {
                        selectedChar = (short) (value.Value.Clamp(0, textBox.Text.Length));
                    }
                }
                else
                {
                    selectedChar = null;
                }
            }
        }

        private readonly TextBox textBox;

        private int textCursor;
        private int? selectedChar;

        public Cursor(TextBox textBox, Color color, Color selection, Rectangle icon, int ticksPerBlink)
        {
            this.textBox = textBox;
            Color = color;
            Selection = selection;
            Icon = icon;
            Active = true;
            visible = false;
            this.ticksPerBlink = ticksPerBlink;
            ticks = 0;
        }

        public void Update()
        {
            ticks++;

            if (ticks <= ticksPerBlink)
            {
                return;
            }

            visible = !visible;
            ticks = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Top left corner of the text area.
            int x = textBox.Renderer.Destination.X;
            int y = textBox.Renderer.Destination.Y;

            Point cp = GetPosition(x, y, TextCursor);
            if (selectedChar.HasValue)
            {
                Point sc = GetPosition(x, y, selectedChar.Value);
                if (sc.X > cp.X)
                {
                    spriteBatch.Draw(Rise.Graphic.GetPixel() ,
                        new Rectangle(cp.X, cp.Y, sc.X - cp.X, textBox.Renderer.Font.LineSpacing), Icon, Selection);
                }
                else
                {
                    spriteBatch.Draw(Rise.Graphic.GetPixel(),
                        new Rectangle(sc.X, sc.Y, cp.X - sc.X, textBox.Renderer.Font.LineSpacing), Icon, Selection);
                }
            }

            if (!visible)
            {
                return;
            }

            spriteBatch.Draw(Rise.Graphic.GetPixel(),
                new Rectangle(cp.X, cp.Y, Icon.Width, textBox.Renderer.Font.LineSpacing), Icon, Color);
        }

        private Point GetPosition(int x, int y, int pos)
        {
            if (pos > 0)
            {
                if (textBox.Text.Characters[pos - 1] == '\n'
                    || textBox.Text.Characters[pos - 1] == '\r')
                {
                    // Beginning of next line.
                    y += textBox.Renderer.Y[pos - 1] + textBox.Renderer.Font.LineSpacing;
                }
                else if (pos == textBox.Text.Length)
                {
                    // After last character.
                    x += textBox.Renderer.X[pos - 1] + textBox.Renderer.Width[pos - 1];
                    y += textBox.Renderer.Y[pos - 1];
                }
                else
                {
                    // Beginning of current character.
                    x += textBox.Renderer.X[pos];
                    y += textBox.Renderer.Y[pos];
                }
            }
            return new Point(x, y);
        }
}
}