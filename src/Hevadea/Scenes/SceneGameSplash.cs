using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;
using Hevadea.Registry;
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

        public override void Load()
        {
            new Thread(() =>
            {
                Ressources.Load();
                REGISTRY.Initialize();
                Directory.CreateDirectory(GLOBAL.GetSavePath());

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
            _sb.Begin(samplerState: SamplerState.PointWrap);
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White);

            if (Ressources.CompanyLogo != null)
            {
                _sb.Draw(Ressources.CompanyLogo, (Rise.Graphic.GetCenter().ToVector2() - Ressources.CompanyLogo.GetCenter()), Color.White);
            }

            _sb.End();
        }
    }
}