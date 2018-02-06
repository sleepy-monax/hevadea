using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Components
{
    public abstract class Scene
    {
        public Widget Container { get; set; }
                
        protected Scene()
        {
            Container = new FloatingContainer();
        }
        
        public virtual string GetDebugInfo()
        {
            return "";
        }

        public void Update(GameTime gameTime)
        {
            if (Container != null)
            {   
                Container.Bound = Engine.Graphic.GetResolutionRect();
                Container?.RefreshLayout();
                Container?.UpdateInternal(gameTime);
            }
            
            OnUpdate(gameTime);
        }
        
        public void Draw()
        {
            // Draw the scene.
            
            // Draw the gui.
        }
        
        public abstract void Load();
        public abstract void OnUpdate(GameTime gameTime);
        public abstract void OnDraw(GameTime gameTime);
        public abstract void Unload();
    }
}