using OpenTK;

namespace WorldOfImagination.Framework.Graphics
{
    public class Camera
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public float Pitch { get; set; } = 0f;
        public float Yaw { get; set; } = 0f;
        public float Roll { get; set; } = 0f;

        Host Host;
        public Camera(Host host)
        {
            Host = host;
        }

        public Matrix4 GetProjectionMatrix()
        {
            //return Matrix4.CreatePerspectiveFieldOfView(1f, Host.Width / Host.Height, 0.1f, 1000f);
            return Matrix4.CreateOrthographic(Host.Width / 100f, Host.Height / 100f, 0.1f, 1000f);
        }

        public Matrix4 GetViewMatrix()
        {
            Vector3 AntiCamPosition = new Vector3(-Position.X, -Position.Y, -Position.Z);
            return Matrix4.Identity * Matrix4.CreateTranslation(AntiCamPosition) * Matrix4.CreateRotationY(Yaw) * Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationZ(Roll);
        }
    }
}
