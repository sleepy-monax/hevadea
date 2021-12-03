using System;
using Hevadea.Framework.Extension;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Hevadea.Framework.Development
{
    public class DebugManager
    {
        private SpriteBatch _sb;
        private float fps;
        private float ups;

        public bool HELP { get; set; }
        public bool GENERAL { get; set; }
        public bool GAME { get; set; }
        public bool UI { get; set; }

        public DebugManager()
        {
            _sb = Rise.Graphic.CreateSpriteBatch();
        }

        public void Update(GameTime gameTime)
        {
            fps += (1f / (Math.Max(1, Rise.MonoGame.DrawTime) / 1000f) - fps) * 0.01f;
            ups += (1f / (Math.Max(1, Rise.MonoGame.UpdateTime) / 1000f) - ups) * 0.01f;
        }

        public void Draw(GameTime gameTime)
        {
            if (GENERAL)
            {
                _sb.Begin();

                var time = MediaPlayer.PlayPosition;
                var songTime = Rise.Sound.PlayingSong?.Duration;

                var text = $@"
FPS and UPS not accurate!
    FPS: {(int) fps}
    UPS: {(int) ups}

    Update: {Rise.MonoGame.UpdateTime}
    Draw:   {Rise.MonoGame.DrawTime}

DEBUG UI: {(HELP ? "HELP " : "")}{(GENERAL ? "GENERAL " : "")}{(GAME ? "GAME " : "")}{(UI ? "UI " : "")}

Playing Effects: {Rise.Sound.SoundEffectInstances.Count}
Playing Song: {Rise.Sound.PlayingSong?.Name ?? "none"} ({time.Humanize()} / {songTime?.Humanize() ?? "00:00"})

Running on platform: '{Rise.Platform.GetPlatformName()}'
    Family: {Rise.Platform.Family}
    Hardware Screen {Rise.Platform.GetScreenWidth()}, {Rise.Platform.GetScreenHeight()}
    Screen: {Rise.Graphic.GetWidth()}, {Rise.Graphic.GetHeight()}
    UiScale: {(int) (Rise.Ui.ScaleFactor * 100)}%

Scene: {Rise.Scene?.GetCurrentScene()?.GetType().Name}
{Rise.Scene?.GetCurrentScene()?.GetDebugInfo() ?? ""}";

                _sb.DrawString(Rise.Ui.DebugFont, text, Rise.Graphic.GetBound(), TextAlignement.Left,
                    TextStyle.Rectangle, Color.White);

                _sb.End();
            }

            if (HELP)
            {
                _sb.Begin();
                var text =
                    @"[F1]: Help [F2, F3, F4]: Toggle debug overlays for Game/General/Ui [F5]: Hide/Show ui [F6/F7]: -/+ Ui Scale";

                _sb.DrawString(Rise.Ui.DebugFont, text, Rise.Graphic.GetBound(), TextAlignement.Bottom,
                    TextStyle.DropShadow, Color.White);
                _sb.End();
            }
        }
    }
}