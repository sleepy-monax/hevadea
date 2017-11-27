using Microsoft.Xna.Framework;
using System;

namespace WorldOfImagination.GameComponent
{
    public class InputManager : Microsoft.Xna.Framework.GameComponent
    {
        public InputManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine($"{nameof(InputManager)} initialized !");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}