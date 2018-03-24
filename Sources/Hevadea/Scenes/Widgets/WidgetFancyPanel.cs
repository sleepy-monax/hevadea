using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetFancyPanel : Panel
    {
        private Sprite _background;

        public WidgetFancyPanel()
        {
            _background = new Sprite(Ressources.TileGui, new Point(4, 0), new Point(2,2));
        }

        public override void RefreshLayout()
        {
            if (Content != null)
            {
                Content.UnitBound = new Padding(32).Apply(UnitBound);
                Content.RefreshLayout();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = Scale(64);

            _background.Draw(spriteBatch, new Rectangle(Bound.X + size / 2, Bound.Y + size / 2, Bound.Width - size, Bound.Height - size), Color.White);
            base.Draw(spriteBatch, gameTime);
            GuiHelper.DrawBox(spriteBatch, Bound, size);
        }
    }
}
