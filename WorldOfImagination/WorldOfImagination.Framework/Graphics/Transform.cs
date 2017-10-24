using OpenTK;

namespace WorldOfImagination.Framework.Graphics
{
    public class Transform
    {
        public Vector3 Translate { get; set; }
        public Vector3 Rotate { get; set; }
        public float Scale { get; set; } = 1f;
        public Transform(Vector3 translate, Vector3 rotate, float scale)
        {
            this.Translate = translate;
            this.Rotate = rotate;
            this.Scale = scale;
        }
        public Transform(Vector3 translate, Vector3 rotate)
        {
            this.Translate = translate;
            this.Rotate = rotate;
        }
        public Transform(Vector3 translate, float scale)
        {
            this.Translate = translate;
            this.Rotate = Vector3.Zero;
            this.Scale = scale;
        }
        public Transform(Vector3 translate)
        {
            this.Translate = translate;
            this.Rotate = Vector3.Zero;
        }
        Matrix4 GetMatrix()
        {
            return Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(Translate);
        }
    }
}