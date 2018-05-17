using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetFancyPanel : Panel
    {
        private Sprite _background;

        public WidgetFancyPanel()
        {
            _background = new Sprite(Ressources.TileGui, new Point(4, 0), new Point(2, 2));
        }

        public override void RefreshLayout()
        {
            if (Content != null)
            {
				Content.UnitBound = UnitBound.Padding(32);
                Content.RefreshLayout();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = Scale(64);

            _background.Draw(spriteBatch, new Rectangle(Bound.X + size / 2, Bound.Y + size / 2, Bound.Width - size, Bound.Height - size), Color.White);
            GuiHelper.DrawBox(spriteBatch, Bound, size);
            base.Draw(spriteBatch, gameTime);
        }
    }
}