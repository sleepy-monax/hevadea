using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Runtime.InteropServices;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Hevadea.Framework.Extension
{
    public static class MonoGameExtension
    {
        public static bool IsLegalCharacter(this SpriteFont font, char c)
        {
            return font.Characters.Contains(c) || c == '\r' || c == '\n';
        }

        public static Vector2 GetCenter(this Texture2D tex)
        {
            return tex.Bounds.Center.ToVector2();
        }

        public static void Clear(this Texture2D tex, Color c)
        {
            var size = tex.Width * tex.Height;
            var data = new Color[size];

            for (var i = 0; i < size; i++) data[i] = c;

            tex.SetData(data);
        }

        public static void SetPixel(this Texture2D tex, int x, int y, Color c)
        {
            if (x >= 0 && x < tex.Width && y >= 0 && y < tex.Height)
            {
                var r = new Rectangle(x, y, 1, 1);
                var color = new Color[] { c };

                tex.SetData(0, r, color, 0, 1);
            }
        }

        public static Texture2D GetTexture2DFromBitmap(GraphicsDevice device, Bitmap bitmap)
        {
            // Buffer size is size of color array multiplied by 4 because
            // each pixel has four color bytes
            int bufferSize = bitmap.Height * bitmap.Width * 4;

            // Create new memory stream and save image to stream so
            // we don't have to save and read file
            System.IO.MemoryStream memoryStream =
                new System.IO.MemoryStream(bufferSize);
            bitmap.Save(memoryStream, ImageFormat.Png);

            // EXCEPTION -->
            Texture2D texture = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, memoryStream);

            return texture;
        }
    }
}