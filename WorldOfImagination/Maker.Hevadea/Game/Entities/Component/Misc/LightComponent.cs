using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class LightComponent : EntityComponent
    {
        public bool On { get; set; } = false;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.White;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
