using Microsoft.Xna.Framework;
using WorldOfImagination.GameComponent.UI;

namespace WorldOfImagination.GameComponent.Scene
{
    public abstract class Scene
    {
        public WorldOfImaginationGame Game;
        public Control UiRoot { get; private set; } = null;

        protected Scene(WorldOfImaginationGame game)
        {
            Game = game;
            UiRoot = new Panel(Game.UI);
        }

        public abstract void Load();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Unload();
    }
}
