using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WorldOfImagination.Game
{
    public class Player : Entity
    {
        public Player(WorldOfImaginationGame game, GameState state) : base(game, state)
        {
            this.Height = 32;
            this.Width = 32;
        }

        public override void OnColide(Entity entity)
        {
 
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Game.Input.KeyDown(Keys.Z))
            {
                SpeedY--;
            }

            if (Game.Input.KeyDown(Keys.S))
            {
                SpeedY++;
            }

            if (Game.Input.KeyDown(Keys.Q))
            {
                SpeedX--;
            }

            if (Game.Input.KeyDown(Keys.D))
            {
                SpeedX++;
            }
        }
    }
}