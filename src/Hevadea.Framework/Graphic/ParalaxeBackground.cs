using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class ParalaxeLayer
    {
        public Texture2D Texture { get; }
        public double Factor { get; }

        public ParalaxeLayer(Texture2D texture, double factor)
        {
            Texture = texture;
            Factor = factor;
        }
    }

    public class ParalaxeBackground
    {
        public ParalaxeLayer[] Layers;
        public double Position = 0;

        public ParalaxeBackground(params ParalaxeLayer[] layers)
        {
            Layers = layers;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, GameTime gameTime)
        {
            Position = Position + gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var l in Layers)
            {
                var onScreenPos = (Position * l.Factor) % destination.Width;

                var dest = new Rectangle(
                    (int)onScreenPos + destination.X, destination.Y,
                    destination.Width,
                    destination.Height
                );

                var dest2 = new Rectangle(
                    (int)onScreenPos - destination.Width + destination.X, destination.Y,
                    destination.Width,
                    destination.Height
                );

                spriteBatch.Draw(l.Texture, dest, Color.White);
                spriteBatch.Draw(l.Texture, dest2, Color.White);
            }
        }
    }
}