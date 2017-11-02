using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using WorldOfImagination.Framework;
using WorldOfImagination.Framework.Graphics;
using WorldOfImagination.Framework.Modules;
using WorldOfImagination.Framework.Primitive;

namespace WorldOfImagination
{
    public class WorldOfImagination : Game
    {
        Texture MakerLogo; 
        Camera Camera;
        Draw Draw;
        Input Input;

        public override void OnLoad()
        {
            Camera = new Camera(Host);
            MakerLogo = Texture.LoadFromFile("Ressources/MakerLogo.png");
            Draw = new Draw();
            Input = new Input();
        }

        public override void OnExit()
        {
           
        }

        float rotx = 0f;
        float roty = 0f;
        float rotz = 0f;

        public override void OnDraw()
        {
            Host.Clear(Color4.Gray);
            Camera.Zoom = 0.1f + (float)Math.Abs(Math.Sin(rotz / 10f));
            Camera.Position = new Vector3(10, 0, 10);
            Draw.Begin(Camera);

            Draw.Rectangle(false, new Rectangle(0, 0, 200, 200), Color4.Red, Transform.Unit);
            
            Draw.Texture(MakerLogo, MakerLogo.Rectangle, MakerLogo.Rectangle, new Vector3(0, 0, 0), new Vector3(0.5f, 0.5f, 0f));

            rotz += 0.01f;
            
            Draw.End();
        }

        public override void OnUpdate(float deltaTime)
        {
            Input.Update(deltaTime);
            Camera.UsePerspective = Input.IsKeyboardKeyDown(Key.Space);
        }
    }
}