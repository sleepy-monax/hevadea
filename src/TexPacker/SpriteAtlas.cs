using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TexPacker
{
    public class SpriteAtlas
    {
        public const int CELL_SIZE = 8;

        public Bitmap Bitmap { get; }
        public Dictionary<string, Sprite> Sprites { get; }
        public Sprite this[string name] { get => Sprites[name]; }

        private Graphics _graphic;
        private bool[,] _freeArea;

        public SpriteAtlas(int width, int height)
        {
            Sprites = new Dictionary<string, Sprite>();
            Bitmap  = new Bitmap(width, height);

            _graphic = Graphics.FromImage(Bitmap);
            _freeArea = new bool[width / CELL_SIZE, height / CELL_SIZE];
        }

        public List<Sprite> InsertSprites(string path)
        {
            Console.WriteLine("Loading sprites from: " + path);

            var sw = new Stopwatch();
            sw.Start();

            var files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            var sprites = new List<Sprite>();

            var bitmaps = new Dictionary<string, Bitmap>();

            foreach (var file in files)
            {
                bitmaps.Add(file, new Bitmap(file));
            }

            sw.Stop();
            Console.WriteLine("Bitmaps loaded from: " + path + " in " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            var keys = bitmaps.Keys.ToList();

            keys.Sort((a, b) => 
            {
                var sizeA = bitmaps[a].Width * bitmaps[a].Height;
                var sizeB = bitmaps[b].Width * bitmaps[b].Height;

                return sizeA.CompareTo(sizeB);
            });

            keys.Reverse();

            foreach (var k in keys)
            {
                var spriteName = k.Replace(path + "\\", "").Replace(".png", "");
                // Console.WriteLine($"[{keys.IndexOf(k) + 1, 3}/{keys.Count}] {spriteName} from {k}");
                InsertSprite(spriteName, bitmaps[k]);
            }

            sw.Stop();
            Console.WriteLine("Sprites Layout from:" + path + " done in " + sw.ElapsedMilliseconds+"ms");

            return sprites;
        }

        Point GetPosition(int width, int height) 
        {
            for (int y = 0; y < Bitmap.Height / CELL_SIZE; y++)
            {
                for (int x = 0; x < Bitmap.Width / CELL_SIZE; x++)
                {
                    var isOk = true;

                    if (!_freeArea[x, y] &&
                        x + Math.Max(1, width / CELL_SIZE) <= Bitmap.Width / CELL_SIZE &&
                        y + Math.Max(1, width / CELL_SIZE) <= Bitmap.Height / CELL_SIZE)
                    {
                        for (int xx = 0; xx < Math.Max(1, width / CELL_SIZE); xx++)
                        {
                            for (int yy = 0; yy < Math.Max(1, height / CELL_SIZE); yy++)
                            {
                                isOk = isOk && !_freeArea[x + xx, y + yy];
                            }
                        }
                    }
                    else
                    {
                        isOk = false;
                    }

                    if (isOk) 
                    {
                        for (int xx = 0; xx < Math.Max(1, width / CELL_SIZE); xx++)
                        {
                            for (int yy = 0; yy < Math.Max(1, height / CELL_SIZE); yy++)
                            {
                                _freeArea[x + xx, y + yy] = true;
                            }
                        }

                        return new Point(x * CELL_SIZE, y * CELL_SIZE);
                    }
                }
            }

            return Point.Empty;
        }

        public void InsertSprite(string name, Bitmap bitmap)
        {
            var position = GetPosition(bitmap.Width, bitmap.Height);
            var sprite = new Sprite(this, name, position.X, position.Y, bitmap.Width, bitmap.Height);
            Sprites.Add(name, sprite);

            _graphic.DrawImageUnscaled(bitmap, position);
        }
    }
}
