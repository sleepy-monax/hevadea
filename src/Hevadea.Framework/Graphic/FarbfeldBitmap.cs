using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class FarbfeldBitmap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private Color[] _pixels;

        public FarbfeldBitmap(int width, int height)
        {
            Width = width;
            Height = height;

            _pixels = new Color[Width * height];
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (Mathf.InRange(0, Width, x) && Mathf.InRange(0, Height, y))
                _pixels[y * Width + x] = color;
        }

        public Texture2D AsTexture()
        {
            var tex = new Texture2D(Rise.MonoGame.GraphicsDevice, Width, Height);
            tex.SetData(_pixels);

            return tex;
        }
    }
}