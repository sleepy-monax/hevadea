using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace Hevadea.Framework.Graphic
{
    public class _Sprite
    {
        [IgnoreDataMember] public _SpriteAtlas Atlas { get; }

        [IgnoreDataMember] public string Name { get; }

        [IgnoreDataMember] public Point Grid { get; }

        [IgnoreDataMember] public Rectangle Bound => new Rectangle(X, Y, Width, Height);

        [IgnoreDataMember] public Vector2 Size => new Vector2(Width, Height);


        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public _Sprite(_SpriteAtlas atlas, string name, int x, int y, int width, int height) : this(atlas, name, x, y, width, height, new Point(1))
        {}

        public _Sprite(_SpriteAtlas atlas, string name, int x, int y, int width, int height, Point grid)
        {
            Atlas = atlas;
            Name = name;

            X = x;
            Y = y;

            Width = width;
            Height = height;

            Grid = grid;
        }

        public _Sprite WithGrid(int width, int height)
        {
            return new _Sprite(Atlas, Name, X, Y, Width, Height, new Point(width, height));
        }

        public _Sprite SubSprite(int x, int y)
        {
            return new _Sprite(
                Atlas, 
                Name,
                X + (Width / Grid.X) * Math.Min(Grid.X - 1, x),
                Y + (Height / Grid.Y) * Math.Min(Grid.Y - 1, y),
                (Width / Grid.X),
                (Height / Grid.Y));
        }

        public _Sprite UpperHalf(float split = 0.5f)
        {
            return new _Sprite(Atlas, Name, X, Y, Width, (int)(Height * split));
        }

        public _Sprite BottomHalf(float split = 0.5f)
        {
            return new _Sprite(Atlas, Name, X, Y + (int)(Height * split), Width, (int)(Height * (1f - split)));
        }
    }
}