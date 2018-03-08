using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class GraphicManager
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private GraphicsDevice _graphicsDevice;

        public RenderTarget2D[] RenderTarget { get; private set; } = new RenderTarget2D[4];

        public GraphicManager(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            _graphicsDeviceManager = graphicsDeviceManager;
            _graphicsDevice = graphicsDevice;
        }
        
        #region Drawing

        public void Clear(Color clearColor)
        {
            Rise.MonoGame.GraphicsDevice.Clear(clearColor);
        }

        private Texture2D _pixel;
        public Texture2D GetPixel()
        {
            if (_pixel == null)
            {
                _pixel = new Texture2D(_graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _pixel.SetData(new[] {Color.White});
            }
            
            return _pixel;
        }

        public SpriteBatch CreateSpriteBatch()
        {
            return new SpriteBatch(_graphicsDevice);
        }
        
        #endregion

        #region Geometry

        public Rectangle GetBound()
        {
            return new Rectangle(0, 0, GetWidth(), GetHeight());
        }

        public Point GetCenter()
        {
            return GetBound().Center;
        }
        
        public Point GetSize()
        {
            return new Point(GetWidth(), GetHeight());
        }

        public int GetWidth()
        {
            return _graphicsDeviceManager.PreferredBackBufferWidth;
        }
        
        public int GetHeight()
        {
            return _graphicsDeviceManager.PreferredBackBufferHeight;
        }
 
        public void SetSize(int sx, int sy)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = sx;
            _graphicsDeviceManager.PreferredBackBufferHeight = sy;
            _graphicsDeviceManager.ApplyChanges();
            ResetRenderTargets();
        }

        public void SetFullscreen()
        {
            _graphicsDeviceManager.IsFullScreen = true;
            _graphicsDeviceManager.ApplyChanges();
        }

        public void SetWindowed()
        {
            _graphicsDeviceManager.IsFullScreen = false;
            _graphicsDeviceManager.ApplyChanges();
        }
        
        public void SetSize(Point size)
        {
            SetSize(size.X, size.Y);
        }

        #endregion

        #region RederTagets

        public void ResetRenderTargets()
        {
            Logger.Log<GraphicManager>($"Reseting render targets... ({GetWidth()}/{GetHeight()})");
            for (var i = 0; i < 4; i++)
            {
                if (RenderTarget[i] != null)
                {
                    RenderTarget[i].Dispose();
                }

                RenderTarget[i] = CreateFullscreenRenderTarget();
            }
        }

        public RenderTarget2D CreateFullscreenRenderTarget()
        {
            Logger.Log<GraphicManager>($"Generating render target {GetWidth()}/{GetHeight()}");
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

        #endregion

        #region Scissorrectangle

        public void SetScissor( Rectangle rect )
        {
            _graphicsDevice.ScissorRectangle = rect;
        }

        public void ResetScissor()
        {
            SetScissor(GetBound());
        }
        
        #endregion
    }
}