using Maker.Rise;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WorldOfImagination.Game.Entities
{
    public class Player : Mob
    {

        WorldOfImaginationGame Game;

        public Player(WorldOfImaginationGame game)
        {
            Game = game;
        }


        public override void Update(GameTime gameTime)
        {
            if (Game.Input.KeyDown(Keys.Z)) { Move(0, -1); }
            if (Game.Input.KeyDown(Keys.S)) { Move(0, 1); }
            if (Game.Input.KeyDown(Keys.Q)) { Move(-1, 0); }
            if (Game.Input.KeyDown(Keys.D)) { Move(1, 0); }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(new Rectangle(Position.X, Position.Y, Width, Height), Color.Red);
        }

    }
}
