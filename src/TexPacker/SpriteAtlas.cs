using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TexPacker
{
    public class SpriteAtlas
    {
        Bitmap _bitmap;
        Graphics _graphic;
        Dictionary<string, Sprite> _sprites;

        public Sprite this[string name] { get => _sprites[name]; }

        public SpriteAtlas(int width, int height)
        {
            _sprites = new Dictionary<string, Sprite>();
            _bitmap  = new Bitmap(width, height);
            _graphic = Graphics.FromImage(_bitmap);
        }

        public List<Sprite> InsertSprites(string path)
        {
            var files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            var sprites = new List<Sprite>();

            foreach (var file in files)
            {
                InsertSprite(file.Replace(path, ""), new Bitmap(file));
            }

            return sprites;
        }

        public Sprite InsertSprite(string name, Bitmap sprite)
        {
            Console.WriteLine($"Loading sprite '{name}'...");

            int x = 0;
            int y = 0;

            // Get the location for the sprite
            

            // Blit the sprite
            _graphic.DrawImageUnscaled(sprite, x, y);

            var s = new Sprite(this, name, x, y, sprite.Width, sprite.Height);
            _sprites.Add(name, s);

            return s;
        }

        public Bitmap SaveImage()
        {
            return _bitmap.Clone(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), PixelFormat.DontCare);
        }
    }
}
