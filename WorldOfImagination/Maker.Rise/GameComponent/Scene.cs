using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;

namespace Maker.Rise.GameComponent
{
    public abstract class Scene
    {
        public WorldOfImaginationGame Game;
        public Control UiRoot { get;}

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
