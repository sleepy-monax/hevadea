using OpenTK;

namespace WorldOfImagination.Framework.Primitive
{
    public class Rectangle
    {

        public float X, Y, Height, Width = 0;

        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
        public Vector2 GetCenter()
        {
            return new Vector2(X + Width / 2, Y + Height / 2);
        }
        public bool Colide(Rectangle rectangle)
        {
            return  X < rectangle.X + rectangle.Width &&
                    rectangle.X < X + Width &&
                    Y < rectangle.Y + rectangle.Height &&
                    rectangle.Y < Y + Height;
        }
    }
}
