using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;

namespace WorldOfImagination.Framework.Graphics.OpenGL
{
    public class Texture
    {
        public string Name { get; set; }
        private int Handle { get; set; } = -1;
        private int Width { get; set; } = 0;
        private int Height { get; set; } = 0;

        public Texture(string name, int handle, int width, int height)
        {
            Name = name;
            Handle = handle;
            Width = width;
            Height = height;
        }

        public void Bind(int textureUnit = 0)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Destroy()
        {
            GL.DeleteTexture(Handle);
            Handle = -1;
        }

        public static Texture LoadFromFile(string fileName, bool UseLinearFiletering = false)
        {
            Bitmap bitmap = new Bitmap(fileName);

            int handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);


            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
            bitmap.UnlockBits(bitmapData);

            if (!UseLinearFiletering)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            }
            else
            {

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                
            }

            return new Texture(fileName, handle, bitmap.Height, bitmap.Width);
        }
    }
}
