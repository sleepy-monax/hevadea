using OpenTK;

namespace WorldOfImagination.Framework.Graphics
{
    public class Transform
    {
        public Vector3 Translate { get; set; }
        public Vector3 Rotation { get; set; }
        public float Scale { get; set; } = 1f;

        public Transform(Vector3 translate, Vector3 Rotation, float scale)
        {
            this.Translate = translate;
            this.Rotation = Rotation;
            this.Scale = scale;
        }

        public Transform(Vector3 translate, Vector3 Rotation)
        {
            this.Translate = translate;
            this.Rotation = Rotation;
        }

        public Transform(Vector3 translate, float scale)
        {
            this.Translate = translate;
            this.Rotation = Vector3.Zero;
            this.Scale = scale;
        }

        public Transform(Vector3 translate)
        {
            this.Translate = translate;
            this.Rotation = Vector3.Zero;
        }

        Matrix4 GetMatrix()
        {
            return Matrix4.Identity *
                   Matrix4.CreateScale(Scale) * 
                   Matrix4.CreateRotationX(Rotation.X) * 
                   Matrix4.CreateRotationY(Rotation.Y) * 
                   Matrix4.CreateRotationZ(Rotation.Z) * 
                   Matrix4.CreateTranslation(Translate);
        }
    }
}