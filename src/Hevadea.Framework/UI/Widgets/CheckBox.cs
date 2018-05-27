using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets
{
    public class CheckBox : Widget
    {
        public bool Checked { get; set; } = false;
        public string Text { get; set; } = "Checkbox";

        public CheckBox()
        {
            MouseClick += (sender) => { Checked = !Checked; };
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        
    }
}
