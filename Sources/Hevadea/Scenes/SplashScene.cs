using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;

namespace Hevadea.Scenes
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
            Directory.CreateDirectory(Rise.Platform.GetStorageFolder() + "/Saves/");
            _sb = Rise.Graphic.CreateSpriteBatch();
            _logo = Ressources.ImgMakerLogo;
            Rise.Ui.DefaultFont = Ressources.FontRomulus;
            Rise.Ui.DebugFont = Ressources.FontHack;
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!(gameTime.TotalGameTime.TotalSeconds > 1) || !_once) return;
            Rise.Scene.Switch(new MainMenu());
            _once = false;
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin();
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White);
            _sb.Draw(_logo, Rise.Graphic.GetCenter().ToVector2() - _logo.GetCenter(), Color.White * 10f);
            _sb.End();
        }
    }
}