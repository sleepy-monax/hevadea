using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.UI
{
    public class PrettyPanel : Panel
    {

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var rect = new Rectangle(Host.Location + new Point(4), Host.Size);
            spriteBatch.FillRectangle(rect, Color.Black * 0.25f);
            spriteBatch.FillRectangle(this.Host, new Color(0, 74, 127));

        }

    }
}
