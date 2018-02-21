using System.IO;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Scenes.Toys;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Maker.Rise.Utils;
using Maker.Utils.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes
{
    public class SplashScene : Scene
    {
        private Texture2D _logo;

        private bool _once = true;
        private SpriteBatch _sb;

        public override void Load()
        {
            // Initialize the game engine
            Ressources.Load();
            REGISTRY.Initialize();
            Engine.SetMouseVisibility(true);
            Directory.CreateDirectory("Saves");
            // Initialize the scene.
            _sb = Engine.Graphic.CreateSpriteBatch();
            _logo = Ressources.ImgMakerLogo;
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!(gameTime.TotalGameTime.TotalSeconds > 1) || !_once) return;
            //Engine.Scene.Switch(new PhysicTest());
            Engine.Scene.Switch(new MainMenu());
            _once = false;
        }

        public override void OnDraw(GameTime gameTime)
        {
            Engine.Graphic.Begin(_sb);
            _sb.FillRectangle(Engine.Graphic.GetResolutionRect(), Color.White);
            _sb.Draw(_logo, Engine.Graphic.GetCenter() - _logo.GetCenter(), Color.White * 10f);
            _sb.End();
        }
    }
}