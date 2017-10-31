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

            Draw.Begin(Camera);

            Draw.Rectangle(false, new Rectangle(0, 0, 200, 200), Color4.Red, Transform.Unit);
            Draw.Texture(MakerLogo, MakerLogo.Rectangle, MakerLogo.Rectangle, new Vector3(rotx, 0, 0), new Vector3(0.5f, 0.5f, 0f));
            Draw.Texture(MakerLogo, MakerLogo.Rectangle, MakerLogo.Rectangle, new Vector3(0, roty, 0), new Vector3(0.5f, 0.5f, 0f));
            Draw.Texture(MakerLogo, MakerLogo.Rectangle, MakerLogo.Rectangle, new Vector3(0, 0, rotz), new Vector3(0.5f, 0.5f, 0f));
            rotx += 0.01f;
            roty += 0.01f;
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