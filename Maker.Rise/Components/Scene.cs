using Maker.Rise.UI;
using Maker.Rise.UI.Containers;
using Maker.Rise.UI.Layout;
using Microsoft.Xna.Framework;

namespace Maker.Rise.Components
{
    public abstract class Scene
    {
        public IContainer Container { get; set; }

        protected Scene()
        {
            Container = new Container<DockLayout>();
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