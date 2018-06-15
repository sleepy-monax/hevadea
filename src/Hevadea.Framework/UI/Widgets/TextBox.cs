using Hevadea.Framework.Utils;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.UI.Widgets
{
    public class TextBox : Widget
    {
        int _cursorIndex;

        public Style  Style { get; set; } = Style.Empty;
        public string Text { get; set; }
        public int    CursorIndex
        {
            get => _cursorIndex;
            set => _cursorIndex = Mathf.Clamp(value, 0, Text.Length);
        }


        public TextBox()
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Style.Draw(spriteBatch, UnitBound);
            spriteBatch.DrawString(Style.Font, Text, Scale(Style.GetContent(UnitBound)), DrawText.Alignement.Left, DrawText.TextStyle.DropShadow, Style.TextColor, Scale(1f));
        }
    }
}
