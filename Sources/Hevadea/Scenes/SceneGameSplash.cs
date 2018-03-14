using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Hevadea.Scenes
{
    public class SceneGameSplash : Scene
    {
        private Texture2D _logo;
        private bool _once = true;
        private SpriteBatch _sb;


        public override void Load()
        {
            Ressources.Load();
            REGISTRY.Initialize();
            Directory.CreateDirectory(Rise.Platform.GetStorageFolder() + "/saves/");
            
            Rise.Ui.DefaultFont = Ressources.FontRomulus;
            Rise.Ui.DebugFont = Ressources.FontHack;

            _sb = Rise.Graphic.CreateSpriteBatch();
            _logo = Ressources.CompanyLogo;
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!(gameTime.TotalGameTime.TotalSeconds > 3) || !_once) return;
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