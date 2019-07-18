using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework
{
    public abstract class Scene
    {
        public Widget Container { get; set; }

        protected Scene()
        {
            Container = new LayoutDock();
        }

        public virtual string GetDebugInfo()
        {
            return "";
        }

        public void RefreshLayout()
        {
            Logger.Log<Scene>("RefreshLayout call!");


            if (Container != null)
            {
                var screenSize = Rise.Graphic.GetSize();
                Container.UnitBound = new Rectangle(new Point(0),
                    new Point((int) (screenSize.X / Rise.Ui.ScaleFactor), (int) (screenSize.Y / Rise.Ui.ScaleFactor)));
                Container.RefreshLayout();
            }
        }

        public void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);

            if (Container != null && Rise.Ui.Enabled) Container.UpdateInternal(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            OnDraw(gameTime);

            if (Container != null && Rise.Ui.Enabled)
            {
                spriteBatch.Begin(new SpriteBatchBeginState
                {
                    SortMode = SpriteSortMode.Immediate, BlendState = BlendState.AlphaBlend,
                    SamplerState = SamplerState.PointWrap,
                    RasterizerState = new RasterizerState() {ScissorTestEnable = true}
                });
                Container.Draw(spriteBatch, gameTime);
                spriteBatch.End();
            }
        }

        public abstract void Load();

        public abstract void OnUpdate(GameTime gameTime);

        public abstract void OnDraw(GameTime gameTime);

        public abstract void Unload();
    }
}