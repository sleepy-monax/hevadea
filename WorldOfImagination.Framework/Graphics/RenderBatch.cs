using System.Collections.Generic;
using OpenTK.Graphics;

namespace WorldOfImagination.Framework.Graphics
{
    public class RenderBatch
    {
        ShaderProgram shader;
        public RenderBatch(ShaderProgram shader)
        {

        }

        public void Begin()
        {
            shader.Use();
        }

        public void Draw(Texture texture, Transform transform, Color4 color)
        {
            
        }

        public void End()
        {
            shader.Stop();
        }
    }
}