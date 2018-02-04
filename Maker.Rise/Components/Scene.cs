using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;

namespace Maker.Rise.Components
{
    public abstract class Scene
    {
        public Container Container { get; set; }

        protected Scene()
        {
            Container = new FloatingContainer();
        }

        public virtual string GetDebugInfo()
        {
            return "";
        }

        public abstract void Load();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Unload();
    }
}