using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Debug
{
    public class DebugManager
    {
        private SpriteBatch _spriteBatch;

        public DebugManager()
        {
            _spriteBatch = Rise.Graphic.CreateSpriteBatch();
        }
        
        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            
            _spriteBatch.DrawString(Rise.Ui.DebugFont, 
            $@"Hevadea
Update: {Rise.MonoGame.UpdateTime}
Draw:   {Rise.MonoGame.DrawTime}
Running on platform: '{Rise.Platform.GetPlatformName()}'
Screen: {Rise.Graphic.GetWidth()}, {Rise.Graphic.GetHeight()} 
{Rise.Scene?.GetCurrentScene()?.GetDebugInfo() ?? ""}", new Vector2(16, 16), Color.White);
            Rise.Pointing.DrawDebug(_spriteBatch);
            _spriteBatch.End();
        }
    }
}