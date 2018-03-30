using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;
using Hevadea.Game.Registry;
using Hevadea.Scenes.MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Threading;

namespace Hevadea.Scenes
{
    public class SceneGameSplash : Scene
    {
        private bool _loadingDone = false;
        private bool _once = true;
        private SpriteBatch _sb;
        private float alpha = 0f;


        public override void Load()
        {
            new Thread(() =>
            {
                Ressources.Load();
                REGISTRY.Initialize();
                Directory.CreateDirectory(Rise.Platform.GetStorageFolder() + "/saves/");
                
                Rise.Ui.DefaultFont = Ressources.FontRomulus;
                Rise.Ui.DebugFont = Ressources.FontHack;
                Thread.Sleep(500);
                _loadingDone = true;
            }).Start();

            _sb = Rise.Graphic.CreateSpriteBatch();
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!_once || !_loadingDone) return;
            Rise.Scene.Switch(new SceneMainMenu());
            _once = false;
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin();
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White);
            
            if (_loadingDone)
            {
                alpha *= 0.95f;
            }
            else
            {
                alpha += (1 - alpha) * 0.95f;
            }
            
            var h = 24 - Mathf.Abs(Mathf.Sin(((float) gameTime.TotalGameTime.TotalSeconds) * Mathf.PI * 3) * 24);
            _sb.FillRectangle(
                Rise.Graphic.GetWidth() / 2f - 16, Rise.Graphic.GetHeight() / 2f - 8 + h / 2f,
                32, 32,
                RiseColor.Accent * alpha,
                Mathf.Sin(((float) gameTime.TotalGameTime.TotalSeconds) * Mathf.PI * 3) / 4, 0.5f, 0.5f);
            
            if (Ressources.CompanyLogo != null)
            {
                //_sb.Draw(Ressources.CompanyLogo, Rise.Graphic.GetCenter().ToVector2() - Ressources.CompanyLogo.GetCenter(), Color.White);
            }
            
            _sb.End();  
        }
    }
}