using Microsoft.Xna.Framework;

namespace Maker.Rise.Components
{
    public abstract class GameComponent
    {
        public RiseGame Game;

        protected GameComponent(RiseGame game)
        {
            Game = game;
        }

        public abstract void Initialize();
        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}