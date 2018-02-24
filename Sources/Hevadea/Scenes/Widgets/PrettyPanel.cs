using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.UI
{
    public class PrettyPanel : Panel
    {

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var rect = new Rectangle(Host.Location + new Point(4), Host.Size);
            spriteBatch.Draw(Engine.Scene.BlurRT, this.Host, this.Host, Color.White);
            spriteBatch.FillRectangle(this.Host, Color.Black * 0.80f);
            spriteBatch.DrawRectangle(Host, Color.White * 0.1f);
        }

    }
}
