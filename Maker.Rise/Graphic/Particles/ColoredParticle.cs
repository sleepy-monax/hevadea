using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Graphic.Particles
{
    public class ColoredParticle : Particle
    {
        public Color Color { get; set; } = Color.White;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.PutPixel(new Vector2(X, Y) - new Vector2(Size, Size) / 2, Color * (Life / FadeOut), Size);
        }
    }
}
