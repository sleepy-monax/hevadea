using Microsoft.Xna.Framework;

namespace WorldOfImagination.GameComponent.Scene
{
    public abstract class Scene
    {
        public Game Game;

        public Scene(Game game)
        {
            Game = game;
        }

        public abstract void Load();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Unload();
    }
}
