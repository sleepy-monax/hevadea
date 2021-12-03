using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DrawingCore;
using System.IO;
using System.Linq;

namespace Hevadea.Framework.Graphic
{
    public class _SpriteAtlas
    {
        public const int CELL_SIZE = 8;

        public Bitmap Bitmap { get; }
        public Texture2D Texture { get; }
        public Dictionary<string, _Sprite> Sprites { get; }
        public _Sprite this[string name]
        {
            get
            {
                if (Sprites.ContainsKey(name))
                {
                    return Sprites[name];
                }
                else
                {
                    return Sprites["none"];
                }
            }
        }

        private readonly Graphics _graphic;
        private readonly bool[,] _freeArea;

        public _SpriteAtlas(int width, int height, string path)
        {
            Sprites = new Dictionary<string, _Sprite>();
            Bitmap = new Bitmap(width, height);

            _graphic = Graphics.FromImage(Bitmap);
            _graphic.ResetTransform();

            _freeArea = new bool[width / CELL_SIZE, height / CELL_SIZE];

            InsertSprites(path);

            Texture = MonoGameExtension.GetTexture2DFromBitmap(Rise.MonoGame.GraphicsDevice, Bitmap);
        }

        public _Sprite GetSprite(string name)
        {
            if (Sprites.ContainsKey(name))
            {
                return Sprites[name];
            }
            else
            {
                return Sprites["none"];
            }
        }

        // FIXME: replace the LikeOperator.LikeString by something in house
        public List<_Sprite> GetSprites(string name)
        {
            var result = new List<_Sprite>();
            foreach (var kv in Sprites)
            {
                if (kv.Key.StartsWith(name))
                {
                    result.Add(kv.Value);
                }
            }

            if (result.Count == 0)
            {
                result.Add(Sprites["none"]);
            }

            return result;
        }

        private List<_Sprite> InsertSprites(string path)
        {
            Logger.Log<_SpriteAtlas>("Loading sprites from: " + path);

            var sw = new Stopwatch();
            sw.Start();

            var files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            var sprites = new List<_Sprite>();

            var bitmaps = new Dictionary<string, Bitmap>();

            foreach (var file in files) bitmaps.Add(file, new Bitmap(file));

            sw.Stop();
            Logger.Log<_SpriteAtlas>("Bitmaps loaded from: " + path + " in " + sw.ElapsedMilliseconds + "ms");
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
                var spriteName = k.Replace(path, "").Replace("\\", "/").Replace(".png", "");
                Logger.Log<_SpriteAtlas>($"[{keys.IndexOf(k) + 1, 3}/{keys.Count}] {spriteName} from {k}");
                InsertSprite(spriteName, bitmaps[k]);
            }

            sw.Stop();
            Logger.Log<_SpriteAtlas>("Sprites Layout from:" + path + " done in " + sw.ElapsedMilliseconds + "ms");

            return sprites;
        }

        private Point GetPosition(int width, int height)
        {
            for (var y = 0; y < Bitmap.Height / CELL_SIZE; y++)
            for (var x = 0; x < Bitmap.Width / CELL_SIZE; x++)
            {
                var isOk = true;

                if (!_freeArea[x, y] &&
                    x + Math.Max(1, width / CELL_SIZE) <= Bitmap.Width / CELL_SIZE &&
                    y + Math.Max(1, width / CELL_SIZE) <= Bitmap.Height / CELL_SIZE)
                    for (var xx = 0; xx < Math.Max(1, width / CELL_SIZE); xx++)
                        for (var yy = 0; yy < Math.Max(1, height / CELL_SIZE); yy++)
                            isOk &= !_freeArea[x + xx, y + yy];
                else
                    isOk = false;

                if (isOk)
                {
                    for (var xx = 0; xx < Math.Max(1, width / CELL_SIZE); xx++)
                    for (var yy = 0; yy < Math.Max(1, height / CELL_SIZE); yy++)
                        _freeArea[x + xx, y + yy] = true;

                    return new Point(x * CELL_SIZE, y * CELL_SIZE);
                }
            }

            return Point.Empty;
        }

        private void InsertSprite(string name, Bitmap bitmap)
        {
            var position = GetPosition(bitmap.Width, bitmap.Height);
            var sprite = new _Sprite(this, name, position.X, position.Y, bitmap.Width, bitmap.Height);
            Sprites.Add(name, sprite);

            _graphic.DrawImage(bitmap, new Rectangle(position, bitmap.Size));
        }
    }
}