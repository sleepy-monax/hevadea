using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Components
{
    public class GraphicComponent
    {
        private GraphicsDeviceManager g;
        private InternalGame Game;

        public GraphicComponent(InternalGame game)
        {
            Game = game;
            g = new GraphicsDeviceManager(game);
        }

        public void SetFullscreen()
        {
            SetWindowedFullScreen();
            g.HardwareModeSwitch = true;
            g.ApplyChanges();
        }

        public void SetWindowed()
        {
            g.IsFullScreen = false;
            g.ApplyChanges();
        }

        public void SetWindowedFullScreen()
        {
            g.HardwareModeSwitch = false;
            g.IsFullScreen = true;
            g.ApplyChanges();
            SetResolution(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public void SetResolution(int width, int height)
        {
            g.PreferredBackBufferWidth = width;
            g.PreferredBackBufferHeight = height;
            g.ApplyChanges();
            Engine.Scene.ResetRenderTargets();
        }

        public Rectangle GetResolutionRect()
        {
            return new Rectangle(Point.Zero, GetResolution());
        }

        public Point GetResolution()
        {
            return new Point(g.PreferredBackBufferWidth, g.PreferredBackBufferHeight);
        }

        public int GetHeight()
        {
            return g.PreferredBackBufferHeight;
        }

        public int GetWidth()
        {
            return g.PreferredBackBufferWidth;
        }

        public Vector2 GetCenter() { return GetResolutionRect().Center.ToVector2(); }

        public RenderTarget2D CreateFullscreenRenderTarget()
        {
            return new RenderTarget2D(GetGraphicsDevice(), Engine.Graphic.GetWidth(),
                Engine.Graphic.GetHeight());
        }

        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            g.GraphicsDevice.SetRenderTarget(renderTarget);   
        }

        public void SetDefaultRenderTarget()
        {
            g.GraphicsDevice.SetRenderTarget(Engine.Scene.RenderTarget);
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return g.GraphicsDevice;
        }

        public SpriteBatch CreateSpriteBatch()
        {
            return new SpriteBatch(g.GraphicsDevice);
        }

        public void Begin(SpriteBatch spriteBatch, bool linearFiltering = true, Matrix? transform = null)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, linearFiltering ? SamplerState.LinearClamp : SamplerState.PointClamp, null, Engine.CommonRasterizerState, null, transform);
        }
    }
}
