using Maker.Rise.Extension;
using Maker.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Graphic.Particles
{
    public class ColoredParticle : Particle
    {
        public Color Color { get; set; } = Color.White;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = Size * Easings.Interpolate(FadeOutAnimation, FadeOutEasing);
            spriteBatch.PutPixel(new Vector2(X, Y) - new Vector2(s, s) / 2, Color, s);
        }
    }
}
