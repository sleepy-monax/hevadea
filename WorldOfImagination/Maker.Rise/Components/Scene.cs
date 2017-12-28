using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Maker.Rise.Components
{
    public abstract class Scene
    {
        public Control UiRoot { get; set; }

        protected Scene()
        {
            UiRoot = new Panel();
        }

        public abstract void Load();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Unload();
    }
}
