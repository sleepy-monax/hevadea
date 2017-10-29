using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using WorldOfImagination.Framework.Primitive;

namespace WorldOfImagination.Framework.Graphics
{
    public class Draw
    {
        ShaderProgram shader;
        VertexArray quad;

        public Draw()
        {
            shader  = new ShaderProgram(File.ReadAllText("Ressources/texture3D.vert"), File.ReadAllText("Ressources/texture3D.frag"));
            quad    = new VertexArray(4);

            quad.SetIndecesBuffer(new int[6] { 0, 1, 2, 0, 2, 3 });

            quad[0] = new Vertex(new Vector3(0f, 0f, 0), new Vector2(0, 0), Color4.White);
            quad[1] = new Vertex(new Vector3(0f, -1f, 0), new Vector2(0, 1), Color4.White);
            quad[2] = new Vertex(new Vector3(1f, -1f, 0), new Vector2(1, 1), Color4.White);
            quad[3] = new Vertex(new Vector3(1f, 0f, 0), new Vector2(1, 0), Color4.White);

            quad.Flush();
        }

        public void Begin(Camera camera)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            quad.Bind();
            shader.Use();
            shader.SetUniformVariable("view", camera.GetViewMatrix());
            shader.SetUniformVariable("projection", camera.GetProjectionMatrix());
        }

        public void Texture(Texture texture, Rectangle sourceRectangle, Rectangle destinationRectangle, Vector3 rotation, Vector3 origine)
        {
            texture.Bind(0);

            shader.SetUniformVariable("transform",      new Transform(origine, new Vector3(destinationRectangle.X / 100, destinationRectangle.Y / 100, -10), rotation, new Vector3(destinationRectangle.Width / 100, destinationRectangle.Height / 100, 1)).GetMatrix());
            shader.SetUniformVariable("tile_position",  new Vector2(sourceRectangle.X / texture.Width, sourceRectangle.Y / texture.Height));
            shader.SetUniformVariable("tile_size",      new Vector2(sourceRectangle.Width / texture.Width, sourceRectangle.Height / texture.Height));

            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }

        public void End()
        {
            shader.Stop();
            quad.Unbind();
        }
    }
}
