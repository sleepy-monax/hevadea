using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.UI
{
    public class Panel : Control
    {
        public Color Color { get; set; } = Color.Transparent;
        
        public Panel(UiManager ui) : base(ui)
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
