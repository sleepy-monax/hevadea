using OpenTK;
using OpenTK.Graphics;

namespace WorldOfImagination.Framework.Graphics
{
    public class Vertex
    {
        public Vector3 Position;
        public Vector2 Texture;
        public Color4 Color;

        public Vertex(Vector3 position, Vector2 texture, Color4 color)
        {
            Position = position;
            Texture = texture;
            Color = color;
        }
    }
}