using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Debug
{
    public class DebugManager
    {
        SpriteBatch _spriteBatch;
		float fps;
        float ups;

        public DebugManager()
        {
            _spriteBatch = Rise.Graphic.CreateSpriteBatch();
        }

        public void Update(GameTime gameTime)
        {
			fps += ((1f / (Math.Max(1, Rise.MonoGame.DrawTime) / 1000f)) - fps) * 0.01f;
			ups += ((1f / (Math.Max(1, Rise.MonoGame.UpdateTime) / 1000f)) - ups) * 0.01f;
		}

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            var text = $@"Hevadea
FPS and UPS not accurate!
    FPS: {(int)fps}
    UPS: {(int)ups}

    Update: {Rise.MonoGame.UpdateTime}
    Draw:   {Rise.MonoGame.DrawTime}

Running on platform: '{Rise.Platform.GetPlatformName()}'
    Family: {Rise.Platform.Family}
    Hardware Screen {Rise.Platform.GetScreenWidth()}, {Rise.Platform.GetScreenHeight()}
    Screen: {Rise.Graphic.GetWidth()}, {Rise.Graphic.GetHeight()}
Scene: {Rise.Scene?.GetCurrentScene()?.GetType().Name} 
{Rise.Scene?.GetCurrentScene()?.GetDebugInfo() ?? ""}";

            _spriteBatch.DrawString(Rise.Ui.DebugFont, text, new Vector2(16, 16 + 1), Color.Black);
            _spriteBatch.DrawString(Rise.Ui.DebugFont, text, new Vector2(16, 16), Color.White);
            Rise.Pointing.DrawDebug(_spriteBatch);
            _spriteBatch.End();
        }
    }
}