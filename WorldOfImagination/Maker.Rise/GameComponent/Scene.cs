using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;

namespace Maker.Rise.GameComponent
{
    public abstract class Scene
    {
        public RiseGame Game;
        public Control UiRoot { get;}

        protected Scene(RiseGame game)
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
