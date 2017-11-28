using Microsoft.Xna.Framework;
using System;

namespace WorldOfImagination.GameComponent
{
    public class AudioManager : Microsoft.Xna.Framework.GameComponent
    {
        public int UpdateTime { get; private set; }

        public AudioManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine($"{nameof(AudioManager)} initialized !");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
