using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic.Particles
{
    public class Color2Particle : Particle
    {
        public Color Color { get; set; } = Color.Red;
        public Color FadingColor { get; set; } = Color.Blue;
        private float maxLife = -1;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (maxLife == -1) maxLife = Life;

            var s = Size * Easing.Interpolate(FadeOutAnimation, FadeOutEasing);
            spriteBatch.PutPixel(new Vector2(X, Y) - new Vector2(s, s) / 2, Color.Lerp(FadingColor, Color, Life /maxLife), s);
        }
    }
}
