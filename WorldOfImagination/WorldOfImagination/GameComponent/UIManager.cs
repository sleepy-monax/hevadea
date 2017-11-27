using Microsoft.Xna.Framework;
using System;

namespace WorldOfImagination.GameComponent
{
    public class UIManager : DrawableGameComponent
    {
        public UIManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine($"{nameof(UIManager)} initialized !");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}