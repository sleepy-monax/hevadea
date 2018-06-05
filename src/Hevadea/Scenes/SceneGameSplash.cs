using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;
using Hevadea.Registry;
using Hevadea.Scenes.MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Threading.Tasks;

namespace Hevadea.Scenes
{
    public class SceneGameSplash : Scene
    {
        private bool _loadingDone;
        private bool _once = true;
        private float _time = 0.1f;

        private SpriteBatch _sb;

        public static bool Initialized = false;

        public override void Load()
        {
            Task.Run(() =>
            {
                if (!Initialized)
                {
                    Ressources.Load();
                    REGISTRY.Initialize();
                    Directory.CreateDirectory(Game.GetSaveFolder());

                    Rise.Ui.DefaultFont = Ressources.FontRomulus;
                    Rise.Ui.DebugFont = Ressources.FontHack;

                    Initialized = true;
                }

                _loadingDone = true;
            });

            _sb = Rise.Graphic.CreateSpriteBatch();
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _time += gameTime.GetDeltaTime();

            if (!_once || !_loadingDone || _time < 2) return;
            Rise.Scene.Switch(new MobileMainMenu());
            _once = false;
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin(samplerState: SamplerState.PointWrap);
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White * Mathf.Clamp01(_time));

            if (Ressources.MakerLogo != null)
            {
                _sb.Draw(Ressources.MakerLogo,
                    Rise.Graphic.GetCenter().ToVector2(), null,
                    new Color(_time, _time, _time), 0f, Ressources.MakerLogo.GetCenter(), Easing.BackEaseOut(Mathf.Clamp01(_time * 8 - 3f)), SpriteEffects.None, 0);
            }

            _sb.End();
        }
    }
}