using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Particles
{
    public class DotParticle : Particle
    {
        private float Time;
        private Color color;
        private Vector2 Speed;

        public DotParticle(Color color, float lifeTime, Vector2 speed)
        {
            Time = lifeTime;
            Height = Width = 1;
            Speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            Time -= gameTime.ElapsedGameTime.Seconds;

            if (Time < 0)
            {
                Remove();
            }

            X += (int)(ConstVal.TileSize * Speed.X);
            Y += (int)(ConstVal.TileSize * Speed.Y);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.PutPixel(new Vector2(X, Y), color);
        }
    }
}
