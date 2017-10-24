using OpenTK;

namespace WorldOfImagination.Framework.Graphics
{
    public class Transform
    {
        public Vector3 Translate { get; set; }
        public Vector3 Rotation { get; set; }
        public float Scale { get; set; } = 1f;

        public Transform(Vector3 translate, Vector3 rotation, float scale)
        {
            Translate = translate;
            Rotation = rotation;
            Scale = scale;
        }

        public Transform(Vector3 translate, Vector3 rotation)
        {
            Translate = translate;
            Rotation = rotation;
        }

        public Transform(Vector3 translate, float scale)
        {
            Translate = translate;
            Rotation = Vector3.Zero;
            Scale = scale;
        }

        public Transform(Vector3 translate)
        {
            Translate = translate;
            Rotation = Vector3.Zero;
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