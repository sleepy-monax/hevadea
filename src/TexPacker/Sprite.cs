using System;
namespace TexPacker
{
    public struct Sprite
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public SpriteAtlas Atlas { get; set; }
        public string Path { get; }

        public Sprite(SpriteAtlas atlas, string path, int x, int y, int width, int height)
        {
            Atlas = atlas ?? throw new ArgumentNullException(nameof(atlas));
            Path = path ?? throw new ArgumentNullException(nameof(path));
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
