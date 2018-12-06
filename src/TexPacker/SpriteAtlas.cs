using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TexPacker
{
    public class SpriteAtlas
    {
        Bitmap _bitmap;
        Graphics _graphic;
        Dictionary<string, Sprite> _sprites;
        bool[,] _freeArea;

        public Sprite this[string name] { get => _sprites[name]; }

        public SpriteAtlas(int width, int height)
        {
            _sprites = new Dictionary<string, Sprite>();
            _bitmap  = new Bitmap(width, height);
            _graphic = Graphics.FromImage(_bitmap);
            _freeArea = new bool[width / 16, height / 16];
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

        Point? GetPosition(int width, int height) 
        {
            for (int x = 0; x < _bitmap.Width / 16; x++)
            {
                for (int y = 0; y < _bitmap.Height / 16; y++)
                {
                    var isOk = true;

                    for (int xx = 0; xx < width / 16; xx++)
                    {
                        for (int yy = 0; yy < height / 16; yy++)
                        {
                            isOk &= !_freeArea[x, y];
                        }
                    }

                    if (isOk) 
                    {
                        for (int xx = 0; xx < width / 16; xx++)
                        {
                            for (int yy = 0; yy < height / 16; yy++)
                            {
                                _freeArea[x, y] = true;
                            }
                        }

                        return new Point(x * 16, y * 16);
                    }
                }
            }

            return null;
        }

        public Sprite? InsertSprite(string name, Bitmap sprite)
        {
            Console.WriteLine($"Loading sprite '{name}'...");

            var p = GetPosition(sprite.Width, sprite.Height) ?? new Point(-1, -1);

            // Blit the sprite
            if (p.X != -1 && p.Y != -1) 
            {
                var s = new Sprite(this, name, p.X, p.Y, sprite.Width, sprite.Height );

                _graphic.DrawImageUnscaled(sprite, p);
                _sprites.Add(name, s);

                return s;
            }

            Console.WriteLine("lol");
            return null;
        }

        public Bitmap SaveImage()
        {
            return _bitmap.Clone(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), PixelFormat.DontCare);
        }
    }
}
