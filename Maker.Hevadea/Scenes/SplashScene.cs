using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Maker.Rise.Ressource;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Maker.Hevadea.Scenes
{
    public class SplashScene : Scene
    {
        private SpriteBatch sb;
        private Texture2D logo;

        public SplashScene()
        {
        }


        public override void Load()
        {
            // Initialize the game engine
            Ressources.Load();
            REGISTRY.Initialize();
            Engine.SetMouseVisibility(true);
            Directory.CreateDirectory("Saves");
            Engine.Graphic.SetResolution(1280, 720);
            //Engine.Graphic.SetResolution(1280, 960);

            // Initialize the scene.
            sb = Engine.Graphic.CreateSpriteBatch();
            logo = Ressources.img_maker_logo;
        }

        public override void Unload()
        {
        }

        bool once = true;
        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 1 && once)
            {
                Engine.Scene.Switch(new EngineSplash());
                once = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.Graphic.Begin(sb);
            sb.FillRectangle(Engine.Graphic.GetResolutionRect(), Color.White);
            sb.Draw(logo,Engine.Graphic.GetCenter() - logo.GetCenter(), Color.White * 10f);
            sb.End();
        }
    }
}