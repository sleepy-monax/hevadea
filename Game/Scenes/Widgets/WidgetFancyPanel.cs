using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetFancyPanel : WidgetPanel
    {
        private Sprite _background;

        public WidgetFancyPanel()
        {
            _background = new Sprite(Resources.TileGui, new Point(4, 0), new Point(2, 2));
        }

        public override void RefreshLayout()
        {
            if (Content != null)
            {
                Content.UnitBound = Padding.Apply(UnitBound.Padding(9));
                Content.RefreshLayout();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = Scale(16 * 3);

            _background.Draw(spriteBatch,
                new Rectangle(Bound.X + Scale(4), Bound.Y + Scale(4), Bound.Width - Scale(8), Bound.Height - Scale(8)),
                Color.White);
            spriteBatch.DrawBox(Bound, size);
            base.Draw(spriteBatch, gameTime);
        }
    }
}