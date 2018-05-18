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
        bool  _loadingDone;
        bool  _once        = true;
        float _alpha       = 0.1f;

		SpriteBatch _sb;

        public override void Load()
        {
            new Thread(() =>
            {
                Ressources.Load();
                REGISTRY.Initialize();
                Directory.CreateDirectory(GLOBAL.GetSavePath());

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
			_alpha += gameTime.GetDeltaTime() * 0.5f;
			_alpha = Mathf.Min(1f, _alpha);

            if (!_once || !_loadingDone || _alpha < 1f) return;
            Rise.Scene.Switch(new SceneMainMenu());
            _once = false;
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin(samplerState: SamplerState.PointWrap);
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White * _alpha);

            if (Ressources.CompanyLogo != null)
            {
				_sb.Draw(
					Ressources.CompanyLogo,
					(Rise.Graphic.GetCenter().ToVector2() - Ressources.CompanyLogo.GetCenter()) 
					    * new Vector2(1f, Easing.CircularEaseOut(_alpha)),
					new Color(_alpha, _alpha, _alpha));
            }

            _sb.End();
        }
    }
}