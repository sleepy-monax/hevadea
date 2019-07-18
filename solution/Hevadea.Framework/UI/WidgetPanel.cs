using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class WidgetPanel : Widget
    {
        public Widget Content { get; set; } = null;

        public override void RefreshLayout()
        {
            if (Content != null && Content.Enabled)
            {
                Content.UnitBound = UnitHost;
                Content.RefreshLayout();
            }
        }

        public override void Update(GameTime gameTime)
        {
            Content?.UpdateInternal(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Content?.DrawIternal(spriteBatch, gameTime);
        }
    }
}