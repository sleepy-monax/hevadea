using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Components
{
    public class GraphicComponent
    {
        private GraphicsDeviceManager g;

        public GraphicComponent(RiseGame game)
        {
            g = new GraphicsDeviceManager(game);
        }

        public void SetFullscreen()
        {
            if (!g.IsFullScreen)
            {
                g.ToggleFullScreen();
            }
        }

        public void SetWindowed()
        {
            if (g.IsFullScreen)
            {
                g.ToggleFullScreen();
            }
        }

        public void SetResolution(int width, int height)
        {
            g.PreferredBackBufferWidth = width;
            g.PreferredBackBufferHeight = height;
            g.ApplyChanges();
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

        public RenderTarget2D CreateRenderTarget()
        {
            return new RenderTarget2D(GetGraphicsDevice(), Engine.Graphic.GetWidth(),
                Engine.Graphic.GetHeight());
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return g.GraphicsDevice;
        }

        public SpriteBatch CreateSpriteBatch()
        {
            return new SpriteBatch(g.GraphicsDevice);
        }
    }
}
