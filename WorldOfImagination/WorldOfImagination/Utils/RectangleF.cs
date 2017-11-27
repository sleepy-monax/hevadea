namespace WorldOfImagination.Utils
{
    public class RectangleF
    {

        public float X, Y, Height, Width = 0;

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }

        public bool Colide(RectangleF rect)
        {
            return X < rect.X + rect.Width &&
                   rect.X < X + Width &&
                   Y < rect.Y + rect.Height &&
                   rect.Y < Y + Height;
             
        }
    }
}
