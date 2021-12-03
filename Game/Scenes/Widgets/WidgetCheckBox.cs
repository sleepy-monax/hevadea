using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetCheckBox : Widget
    {
        public bool Checked { get; set; } = false;
        public string Text { get; set; } = "Checkbox";

        private Sprite _check;
        private Sprite _unChecked;

        public WidgetCheckBox()
        {
            _check = new Sprite(Resources.TileGui, new Point(5, 7));
            _unChecked = new Sprite(Resources.TileGui, new Point(6, 7));

            MouseClick += (sender) => { Checked = !Checked; };
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var pos = Bound.Height / 2 - Scale(24);
            var dest = new Rectangle(Bound.X + pos, Bound.Y + pos, Scale(48), Scale(48));

            if (Checked)
                _check.Draw(spriteBatch, dest, Color.White);
            else
                _unChecked.Draw(spriteBatch, dest, Color.White);

            spriteBatch.DrawString(Resources.FontRomulus, Text,
                new Rectangle(Bound.X + Bound.Height, Bound.Y, Bound.Width - Bound.Height, Bound.Height),
                TextAlignement.Left, TextStyle.DropShadow, Color.White, Scale(1f));
        }
    }
}