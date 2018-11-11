using System.IO;
using Hevadea.Framework.Data;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class FarbfeldBitmap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        Color[] _pixels;

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

        /* 
        public static FarbfeldBitmap FromStream(Stream stream)
        {
            byte[] header = new byte[16];
            stream.Read(header, 0, 16);
            BufferReader reader = new BufferReader(header);

            reader.ReadStringASCII(out var magic)
                  .ReadInteger(out var width)
                  .ReadInteger(out var height);

            if (magic == "farbfeld")
            {
                throw new InvalidDataException();
            }

            if (width == 0 || height == 0)
            {
                return null;
            }

            FarbfeldBitmap bmp = new FarbfeldBitmap(width, height);



            return bmp;
        }
        */
    }
}