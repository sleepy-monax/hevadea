using System;
using OpenTK;
using OpenTK.Graphics;
using WorldOfImagination.Framework;
using WorldOfImagination.Framework.Graphics;

namespace WorldOfImagination 
{
    public class WorldOfImagination : Game
    {
        Texture test; 
        VertexArray v ;
        ShaderProgram p;
        public override void OnDraw()
        {
            Host.Clear(Color4.DodgerBlue);
            v.Bind();
            p.Use();
            test.Bind();

            p.Stop();
            v.Unbind();   
        }

        public override void OnExit()
        {
            
        }

        public override void OnLoad()
        {
            test = Texture.LoadFromFile("./test.png");
            v = new VertexArray(4);
            v.SetIndecesBuffer(new int[6]{0, 1, 2, 0, 2, 3});
            v[0] = new Vertex(new Vector3(0, 0, 0), new Vector2(0, 0), Color4.White);
            v[1] = new Vertex(new Vector3(0, 1, 0), new Vector2(0, 1), Color4.White);
            v[2] = new Vertex(new Vector3(1, 1, 0), new Vector2(1, 1), Color4.White);
            v[3] = new Vertex(new Vector3(1, 0, 0), new Vector2(1, 0), Color4.White);
            v.Flush();
            p = new ShaderProgram(ShaderCode.vertexShader, ShaderCode.fragmentShader);
        }

        public override void OnUpdate(double deltaTime)
        {
            
        }
    }
}