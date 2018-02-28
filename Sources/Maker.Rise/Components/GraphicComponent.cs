using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Components
{
    public class GraphicComponent
    {
        internal GraphicsDeviceManager g;
        private InternalGame Game;

        public RenderTarget2D[] RenderTarget = new RenderTarget2D[4];
        
        public GraphicComponent(InternalGame game)
        {
            Game = game;
            g = new GraphicsDeviceManager(game);
        }


        public void ResetRenderTargets()
        {
            Logger.Log<GraphicComponent>($"Reseting render targets... ({GetWidth()}/{GetHeight()})");
            for (var i = 0; i < 4; i++)
            {
                if (RenderTarget[i] != null)
                {
                    RenderTarget[i].Dispose();
                }

                RenderTarget[i] = CreateFullscreenRenderTarget();
            }
        }

        public void SetFullscreen()
        {
            g.HardwareModeSwitch = true;
            g.IsFullScreen = true;
            g.SynchronizeWithVerticalRetrace = true;
            SetResolution(Engine.Platform.GetHardwareWidth(), Engine.Platform.GetHardwareHeight());
            g.ApplyChanges();
        }

        public void SetWindowed()
        {
            g.IsFullScreen = false;
            g.ApplyChanges();
        }


        public void SetResolution(int width, int height)
        {
            g.PreferredBackBufferWidth = width;
            g.PreferredBackBufferHeight = height;
            g.ApplyChanges();
            ResetRenderTargets();
        }

        public Rectangle GetResolutionRect()
        {
            return new Rectangle(Point.Zero, GetResolution());
        }

        public Point GetResolution()
        {
            return new Point(GetWidth(), GetHeight());
        }

        public int GetWidth()
        {
            return g.PreferredBackBufferWidth;
        }

        public int GetHeight()
        {
            return g.PreferredBackBufferHeight;
        }


        public Vector2 GetCenter() { return GetResolutionRect().Center.ToVector2(); }

        public RenderTarget2D CreateFullscreenRenderTarget()
        {
            Logger.Log<GraphicComponent>($"Generating render target {GetWidth()}/{GetHeight()}");
            return new RenderTarget2D(GetGraphicsDevice(), GetWidth(), GetHeight());
        }

        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            g.GraphicsDevice.SetRenderTarget(renderTarget);   
        }

        public void SetDefaultRenderTarget()
        {
            g.GraphicsDevice.SetRenderTarget(null);
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
