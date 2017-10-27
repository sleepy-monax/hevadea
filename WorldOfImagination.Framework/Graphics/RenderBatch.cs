using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfImagination.Framework.Graphics
{
    public class RenderBatch
    {
        ShaderProgram Shader;
        Dictionary<Texture, Transform> Batch;
        Camera Camera;
        public RenderBatch(ShaderProgram shader)
        {
            Shader = shader;
        }

        public void Begin(Camera camera)
        {
            Shader.Use();
            Camera = camera;
        }

        public void Draw(Texture texture, Transform transform, Color4 color)
        {
            Batch.Add(texture, transform);
        }

        public void End()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            
            // Render the batch here.

            Shader.Stop();
            Batch.Clear();
        }
    }
}