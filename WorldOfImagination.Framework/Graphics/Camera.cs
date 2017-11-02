using OpenTK;

namespace WorldOfImagination.Framework.Graphics
{
    public class Camera
    {
        public Vector3 Position { get; set; } = new Vector3(0, 0, 10);
        public float Pitch { get; set; } = 0f;
        public float Yaw { get; set; } = 0f;
        public float Roll { get; set; } = 0f;
        public float FOV { get; set; } = 1f;
        public float Zoom { get; set; } = 1f;
        public float NearPlane { get; set; } = 0.1f;
        public bool UsePerspective = false;

        Host Host;
        public Camera(Host host, bool usePerspective = false, float fov = 1f)
        {
            Host = host;
            UsePerspective = usePerspective;
            FOV = fov;
        }

        public Matrix4 GetProjectionMatrix()
        {
            if (UsePerspective)
                return Matrix4.CreatePerspectiveFieldOfView(FOV, (float)Host.Width / (float)Host.Height, NearPlane, 1000f);
            
            return Matrix4.CreateOrthographic(Host.Width / 100f, Host.Height / 100f, NearPlane, 1000f);
        }

        public Matrix4 GetViewMatrix()
        {
            Vector3 AntiCamPosition = new Vector3(-Position.X * Zoom, -Position.Y * Zoom, -Position.Z * Zoom);
            return Matrix4.CreateTranslation(AntiCamPosition)* Matrix4.CreateScale(Zoom) * Matrix4.CreateRotationY(Yaw) * Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationZ(Roll);
        }

        public void Move(Vector3 center)
        {
            if (UsePerspective)
            {
                Position = center;
            }
            else
            {
                Position = center + new Vector3(-Host.Width / 2f,-Host.Height / 2f,0);
            }
        }

        public Vector3 ToWorldSpace(Vector2 ScreenSpace)
        {
            return new Vector3(ScreenSpace.X / Zoom + Position.X,ScreenSpace.Y / Zoom + Position.Y, NearPlane);
        }

        public void LookAt(Vector3 position)
        {

        }
    }
}
