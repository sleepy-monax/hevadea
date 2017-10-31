using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using WorldOfImagination.Framework.Graphics;
using WorldOfImagination.Framework.Primitive;

namespace WorldOfImagination.Framework.Modules
{
    public class Draw
    {
        ShaderProgram shader;
        ShaderProgram colorShader;
        VertexArray quad;
        VertexArray rectangle;
        VertexArray point;
        Camera Camera;
        public Draw(string glslVersion = "320 es")
        {
            shader  = new ShaderProgram(File.ReadAllText("Ressources/common.vert"), File.ReadAllText("Ressources/texture.frag"), glslVersion);
            colorShader = new ShaderProgram(File.ReadAllText("Ressources/common.vert"), File.ReadAllText("Ressources/color.frag"), glslVersion);
            quad    = new VertexArray(4);
            rectangle = new VertexArray(4);

            quad.SetIndecesBuffer(new int[6] { 0, 1, 2, 0, 2, 3 });
            rectangle.SetIndecesBuffer(new int[8] {0, 1, 1, 2,  2, 3, 3, 0});

            quad[0] = new Vertex(new Vector3(0f, 1f, 0), new Vector2(0, 0), Color4.White);
            quad[1] = new Vertex(new Vector3(0f, 0f, 0), new Vector2(0, 1), Color4.White);
            quad[2] = new Vertex(new Vector3(1f, 0f, 0), new Vector2(1, 1), Color4.White);
            quad[3] = new Vertex(new Vector3(1f, 1f, 0), new Vector2(1, 0), Color4.White);

            rectangle[0] = new Vertex(new Vector3(0f, 1f, 0), new Vector2(0, 0), Color4.White);
            rectangle[1] = new Vertex(new Vector3(0f, 0f, 0), new Vector2(0, 1), Color4.White);
            rectangle[2] = new Vertex(new Vector3(1f, 0f, 0), new Vector2(1, 1), Color4.White);
            rectangle[3] = new Vertex(new Vector3(1f, 1f, 0), new Vector2(1, 0), Color4.White);

            quad.Flush();
            rectangle.Flush();
        }

        public void Begin(Camera camera)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            Camera = camera;
        }

        public void Texture(Texture texture, Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            Texture(texture, sourceRectangle, destinationRectangle, Vector3.Zero, Vector3.Zero);
        }

        public void Texture(Texture texture, Rectangle sourceRectangle, Rectangle destinationRectangle, Vector3 rotation, Vector3 origine)
        {
            quad.Bind();
            shader.Use();
            texture.Bind(0);
            
            shader.SetUniformVariable("view", Camera.GetViewMatrix());
            shader.SetUniformVariable("projection", Camera.GetProjectionMatrix());
            shader.SetUniformVariable("transform", new Transform(origine, new Vector3(destinationRectangle.X / 100, destinationRectangle.Y / 100, 0), rotation, new Vector3(destinationRectangle.Width / 100, destinationRectangle.Height / 100, 1)).GetMatrix());
            shader.SetUniformVariable("tile_position", new Vector2(sourceRectangle.X / texture.Width, sourceRectangle.Y / texture.Height));
            shader.SetUniformVariable("tile_size", new Vector2(sourceRectangle.Width / texture.Width, sourceRectangle.Height / texture.Height));

            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            shader.Stop();
            quad.Unbind();
        }

        public void Rectangle(bool fill, Rectangle rect, Color4 color, Transform transform)
        {

            colorShader.Use();
            shader.SetUniformVariable("view", Camera.GetViewMatrix());
            shader.SetUniformVariable("projection", Camera.GetProjectionMatrix());
            colorShader.SetUniformVariable("material_color", color);
            colorShader.SetUniformVariable("transform", transform.GetMatrix());
            
            if (fill)
            {
                quad.Bind();
                GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
                quad.Unbind();
            }
            else
            {
                rectangle.Bind();
                GL.DrawElements(BeginMode.Lines, 8, DrawElementsType.UnsignedInt, 0);
                rectangle.Unbind();
            }

            colorShader.Stop();
        }

        public void End()
        {

        }
    }
}
