using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Ressource
{
    public class ParalaxeLayer
    {
        public readonly Texture2D Texture;
        public readonly float Factor;


        public ParalaxeLayer(Texture2D texture, float factor)
        {
            Texture = texture;
            Factor = factor;
        }
    }

    public class ParalaxeBackground
    {
        public ParalaxeLayer[] Layers;
        public float Position = 0;

        public ParalaxeBackground(params ParalaxeLayer[] layers)
        {
            Layers = layers;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Position = Position + (gameTime.ElapsedGameTime.Milliseconds / 1000f) * 32;
            foreach (var l in Layers)
            {
                var onScreenPos = (Position * l.Factor) % Engine.Graphic.GetWidth();


                var dest = new Rectangle(
                    (int) onScreenPos, 0,
                    (int) (Engine.Graphic.GetWidth()),
                    (int) (Engine.Graphic.GetHeight())
                );

                var dest2 = new Rectangle(
                    (int) onScreenPos - Engine.Graphic.GetWidth(), 0,
                    (int) (Engine.Graphic.GetWidth()),
                    (int) (Engine.Graphic.GetHeight())
                );

                spriteBatch.Draw(l.Texture, dest, Color.White);
                spriteBatch.Draw(l.Texture, dest2, Color.White);
            }
        }
    }
}