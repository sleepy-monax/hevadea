using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Threading;
using Hevadea.Registry;

namespace Hevadea.Scenes
{
    public class SceneGameSplash : Scene
    {
        private bool _loadingDone = false;
        private bool _once = true;
        private SpriteBatch _sb;
        private float _alpha = 0f;


        public override void Load()
        {
            new Thread(() =>
            {
                Ressources.Load();
                REGISTRY.Initialize();
                Directory.CreateDirectory(Rise.Platform.GetStorageFolder() + "/saves/");
                
                Rise.Ui.DefaultFont = Ressources.FontRomulus;
                Rise.Ui.DebugFont = Ressources.FontHack;
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

        float r = 0;

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin(samplerState: SamplerState.PointWrap);
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White);
            
            if (_loadingDone)
            {
                _alpha *= 0.95f;
            }
            else
            {
                _alpha += (1 - _alpha) * 0.95f;
            }

            var f = (Mathf.Sin((float)gameTime.TotalGameTime.TotalSeconds * Mathf.PI) + 1);
            r += f * 0.05f;
            _sb.FillRectangle(
                Rise.Graphic.GetWidth() / 2, Rise.Graphic.GetHeight() / 2,
                128 * f, 128 * f,
                ColorPalette.Accent * _alpha,r
                , 0.5f, 0.5f);
            
            if (Ressources.FontRomulus != null)
            {
                _sb.DrawString(Ressources.FontRomulus, "Loading...", Rise.Graphic.GetBound(), DrawText.Alignement.Center, DrawText.TextStyle.Regular, Color.Black, 2f);
            }
            
            _sb.End();  
        }
    }
}