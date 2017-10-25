using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using WorldOfImagination.Framework;
using WorldOfImagination.Framework.Graphics;

namespace WorldOfImagination
{
    public class WorldOfImagination : Game
    {
        Texture test; 
        VertexArray v ;
        ShaderProgram p;
        Camera c;
        public override void OnDraw()
        {
            GL.Enable(EnableCap.DepthTest);
            Host.Clear(Color4.Black);
            v.Bind();
            test.Bind(0);
            p.Use();
            p.SetUniformVariable("view", c.GetViewMatrix());
            p.SetUniformVariable("projection", c.GetProjectionMatrix());
            p.SetUniformVariable("transform", new Transform(new Vector3(-test.Width / 2, test.Height / 2, 0), Vector3.Zero, new Vector3(test.Width, test.Height, 1)).GetMatrix());
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            p.Stop();
            v.Unbind();   
        }

        public override void OnExit()
        {
            
        }

        public override void OnLoad()
        {
            test = Texture.LoadFromFile("test.png");
            v = new VertexArray(4);
            v.SetIndecesBuffer(new int[6]{0, 1, 2, 0, 2, 3});

            v[0] = new Vertex(new Vector3(0f, 0f, -2), new Vector2(0, 0), Color4.White);
            v[1] = new Vertex(new Vector3(0f, -1f, -2), new Vector2(0, 1), Color4.White);
            v[2] = new Vertex(new Vector3(1f, -1f, -2), new Vector2(1, 1), Color4.White);
            v[3] = new Vertex(new Vector3(1f, 0f, -2), new Vector2(1, 0), Color4.White);

            v.Flush();

            p = new ShaderProgram(File.ReadAllText("Ressources/texture3D.vert"), File.ReadAllText("Ressources/texture3D.frag"));
            c = new Camera(this.Host);
        }

        public override void OnUpdate(double deltaTime)
        {
            
        }
    }
}