using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TexPacker
{
    public struct Sprite
    {
        [IgnoreDataMember] public SpriteAtlas Atlas { get; }

        [IgnoreDataMember] public string Name { get; }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Sprite(SpriteAtlas atlas, string Name, int x, int y, int width, int height) : this()
        {
            Atlas = atlas;
            this.Name = Name;

            X = x;
            Y = y;

            Width = width;
            Height = height;
        }
    }
}