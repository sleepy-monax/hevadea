using System;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hevadea.Scenes
{
    public class SceneLoadingScreen : Scene
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
                try
                {
                    if (!Initialized)
                    {
                        Game.Initialize();

                        Initialized = true;
                    }

                    _loadingDone = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
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
            Game.GoToTileScreen();
            _once = false;
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin(samplerState: SamplerState.PointWrap);
            _sb.FillRectangle(Rise.Graphic.GetBound(), Color.White * Mathf.Clamp01(_time));

            if (Resources.MakerLogo != null)
                _sb.Draw(Resources.MakerLogo,
                    Rise.Graphic.GetCenter().ToVector2(), null,
                    new Color(_time, _time, _time), 0f, Resources.MakerLogo.GetCenter(),
                    Easing.BackEaseOut(Mathf.Clamp01(_time * 8 - 3f)) * 4, SpriteEffects.None, 0);

            _sb.FillRectangle(new Vector2(0, Rise.Graphic.GetHeight() - 16),
                new Vector2(Rise.Graphic.GetWidth() * (_time / 2f), 16), ColorPalette.Accent);

            _sb.End();
        }
    }
}