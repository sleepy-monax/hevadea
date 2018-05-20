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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Position = Position + gameTime.ElapsedGameTime.TotalSeconds * 0.1f;
            float screenRation = Rise.Graphic.GetWidth() / (float)Rise.Graphic.GetHeight();

            foreach (var l in Layers)
            {
                float scale = 0;
                float layerRation = l.Texture.Width / (float)l.Texture.Height;


                if (screenRation > layerRation)
                {
                    scale = Rise.Graphic.GetWidth() / (float)l.Texture.Width;
                }
                else
                {
                    scale = Rise.Graphic.GetHeight() / (float)l.Texture.Height;
                }



                float w = l.Texture.Width * scale;
                float h = l.Texture.Height * scale;

                var onScreenPos = (Position * l.Factor) % l.Texture.Width;

                var dest = new Rectangle(
                    (int)(onScreenPos * scale), 0,
                    (int)w,
                    (int)h
                );

                var dest2 = new Rectangle(
                    (int)(onScreenPos * scale - w), 0,
                    (int)w,
                    (int)h
                );

                spriteBatch.Draw(l.Texture, dest, Color.White);
                spriteBatch.Draw(l.Texture, dest2, Color.White);
            }
        }
    }
}