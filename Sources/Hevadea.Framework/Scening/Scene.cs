using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Scening
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
                Container.Bound = new Rectangle(new Point(0), Rise.Graphic.GetSize());
                Container?.RefreshLayout();
                Container?.UpdateInternal(gameTime);
            }
            
            OnUpdate(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            OnDraw(gameTime);
            Container?.Draw(spriteBatch, gameTime);
        }
        
        public abstract void Load();
        public abstract void OnUpdate(GameTime gameTime);
        public abstract void OnDraw(GameTime gameTime);
        public abstract void Unload();
    }
}