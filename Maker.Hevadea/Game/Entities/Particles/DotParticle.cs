using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Particles
{
    public class DotParticle : Particle
    {
        private readonly Color Color;
        private Vector2 Speed;
        private float Time;

        public DotParticle(Color color, float lifeTime, Vector2 speed)
        {
            Time = lifeTime;
            Height = Width = 1;
            Speed = speed;
            Color = color;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Time -= gameTime.ElapsedGameTime.Seconds;

            if (Time < 0) Remove();

            SetPosition((int) (ConstVal.TileSize * Speed.X),
                (int) (ConstVal.TileSize * Speed.Y));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color);
        }
    }
}