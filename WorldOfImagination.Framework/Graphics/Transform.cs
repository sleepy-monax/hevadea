using OpenTK;

namespace WorldOfImagination.Framework.Graphics
{
    public class Transform
    {
        public Vector3  Origine {get; set;} = Vector3.Zero;
        public Vector3  Translate   { get; set; }
        public Vector3  Rotation    { get; set; }
        public Vector3  Scale       { get; set; } = Vector3.Zero;

        public static Transform Unit { get { return new Transform(Vector3.Zero, Vector3.Zero, Vector3.One); } }

        public Transform(Vector3 translate, Vector3 rotation, Vector3 scale)
        {
            Translate = translate;
            Rotation = rotation;
            Scale = scale;
        }


        public Transform(Vector3 translate, Vector3 scale)
        {
            Translate = translate;
            Rotation = Vector3.Zero;
            Scale = scale;
        }

        public Transform(Vector3 origine, Vector3 translate, Vector3 rotation, Vector3 scale)
        {
            Origine = origine;
            Translate = translate;
            Rotation = rotation;
            Scale = scale;
        }

        public Matrix4 GetMatrix()
        {

            return Matrix4.CreateTranslation(-Origine) *
                   Matrix4.CreateScale(Scale) * 
                   Matrix4.CreateRotationX(Rotation.X) *
                   Matrix4.CreateRotationY(Rotation.Y) *
                   Matrix4.CreateRotationZ(Rotation.Z) *
                   Matrix4.CreateTranslation(Translate);
        }
    }
}