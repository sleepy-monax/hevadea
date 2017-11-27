using Microsoft.Xna.Framework;
using System;

namespace WorldOfImagination.GameComponent
{
    public class NetworkManager : Microsoft.Xna.Framework.GameComponent
    {
        public NetworkManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine($"{nameof(NetworkManager)} initialized !");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
