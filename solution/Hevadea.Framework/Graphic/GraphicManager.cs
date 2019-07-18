using Hevadea.Framework.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Framework.Graphic
{
    public class GraphicManager
    {
        private Texture2D _pixel;
        private Texture2D _fallbackTexture;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private GraphicsDevice _graphicsDevice;

        public RenderTarget2D[] RenderTarget { get; private set; } = new RenderTarget2D[4];

        public GraphicManager(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            _graphicsDeviceManager = graphicsDeviceManager;
            _graphicsDevice = graphicsDevice;
        }

        public Texture2D GetPixel()
        {
            if (_pixel == null)
            {
                _pixel = new Texture2D(_graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _pixel.SetData(new[] {Color.White});
            }

            return _pixel;
        }

        public Texture2D GetFallbackTexture()
        {
            if (_fallbackTexture == null)
            {
                _fallbackTexture = new Texture2D(_graphicsDevice, 2, 2, false, SurfaceFormat.Color);
                _fallbackTexture.SetData(new[]
                {
                    Color.Black, Color.Magenta,
                    Color.Magenta, Color.Black
                });
            }

            return _fallbackTexture;
        }

        public void Clear(Color clearColor)
        {
            Rise.MonoGame.GraphicsDevice.Clear(clearColor);
        }

        public void SetFullscreen(int width, int height)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = width;
            _graphicsDeviceManager.PreferredBackBufferHeight = height;
            _graphicsDeviceManager.IsFullScreen = true;
            _graphicsDeviceManager.ApplyChanges();

            ResetRenderTargets();
        }

        public void SetFullscreen()
        {
            _graphicsDeviceManager.IsFullScreen = true;
            _graphicsDeviceManager.ApplyChanges();

            ResetRenderTargets();
        }

        public void SetWindowed()
        {
            _graphicsDeviceManager.IsFullScreen = false;
            _graphicsDeviceManager.ApplyChanges();

            ResetRenderTargets();
        }

        /* --- Render targets ---------------------------------------------- */

        public void ResetRenderTargets()
        {
            Logger.Log<GraphicManager>($"Reseting render targets... ({GetWidth()}/{GetHeight()})");
            for (var i = 0; i < 4; i++)
            {
                if (RenderTarget[i] != null) RenderTarget[i].Dispose();

                RenderTarget[i] = CreateFullscreenRenderTarget(i);
            }
        }

        public SpriteBatch CreateSpriteBatch()
        {
            return Rise.NoGraphic ? null : new SpriteBatch(_graphicsDevice);
        }

        public RenderTarget2D CreateRenderTarget(int width, int height)
        {
            return new RenderTarget2D(_graphicsDevice, width, height);
        }

        public RenderTarget2D CreateFullscreenRenderTarget(int id)
        {
            Logger.Log<GraphicManager>($"Generating fullscreen render target id:{id} {GetWidth()}/{GetHeight()}");
            return new RenderTarget2D(_graphicsDevice, GetWidth(), GetHeight());
        }

        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            _graphicsDevice.SetRenderTarget(renderTarget);
        }

        public void SetDefaultRenderTarget()
        {
            _graphicsDevice.SetRenderTarget(null);
        }

        public void SetScissor(Rectangle rect)
        {
            _graphicsDevice.ScissorRectangle = rect;
        }

        public void ResetScissor()
        {
            SetScissor(GetBound());
        }

        /* --- Screen size ------------------------------------------------- */

        public int GetWidth()
        {
            return Rise.Platform.Family == PlatformFamily.Mobile
                ? Rise.Platform.GetScreenWidth()
                : _graphicsDeviceManager.PreferredBackBufferWidth;
        }

        public int GetHeight()
        {
            return Rise.Platform.Family == PlatformFamily.Mobile
                ? Rise.Platform.GetScreenHeight()
                : _graphicsDeviceManager.PreferredBackBufferHeight;
        }

        public Point GetSize()
        {
            return new Point(GetWidth(), GetHeight());
        }

        public Point GetCenter()
        {
            return GetBound().Center;
        }

        public Rectangle GetBound()
        {
            return new Rectangle(new Point(0), GetSize());
        }

        public void SetSize(Point size)
        {
            SetSize(size.X, size.Y);
        }

        public void SetSize(int sx, int sy)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = sx;
            _graphicsDeviceManager.PreferredBackBufferHeight = sy;
            _graphicsDeviceManager.ApplyChanges();
            ResetRenderTargets();
            Rise.Ui.RefreshLayout();
        }

        public void AllowUserResizing()
        {
            Rise.MonoGame.Window.AllowUserResizing = true;
            Rise.MonoGame.Window.ClientSizeChanged += HandleClientSizeChanged;
        }

        public void HandleClientSizeChanged(object sender, EventArgs args)
        {
            SetSize(new Point(Rise.MonoGame.Window.ClientBounds.Width, Rise.MonoGame.Window.ClientBounds.Height));
        }
    }
}