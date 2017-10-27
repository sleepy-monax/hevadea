using OpenTK;
using OpenTK.Graphics;
using WorldOfImagination.Framework;
using WorldOfImagination.Framework.Graphics;

namespace WorldOfImagination
{
    public class WorldOfImagination : Game
    {
        Texture MakerLogo; 
        Camera Camera;
        Draw Draw;

        public override void OnLoad()
        {
            Camera = new Camera(Host);
            MakerLogo = Texture.LoadFromFile("Ressources/MakerLogo.png");
            Draw = new Draw();
        }

        public override void OnExit()
        {
            
        }

        Vector3 rot;

        public override void OnDraw()
        {
            Host.Clear(Color4.Gray);

            Draw.Begin(Camera);

            Draw.Texture(MakerLogo, MakerLogo.Rectangle, MakerLogo.Rectangle, rot);
            //rot.X += 0.01f;
            //rot.Y += 0.01f;
            //rot.Z += 0.01f;

            Draw.End();
        }


        public override void OnUpdate(double deltaTime)
        {
            
        }
    }
}