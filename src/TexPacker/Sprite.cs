using System;
using System.Drawing;

namespace TexPacker
{
    public struct Sprite
    {
        public SpriteAtlas Atlas { get; set; }
        public string Path { get; }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Sprite(SpriteAtlas atlas, string path, int x, int y, int width, int height) : this()
        {
            Atlas = atlas;
            Path = path;

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
