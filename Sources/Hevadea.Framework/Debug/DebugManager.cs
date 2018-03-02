using System.Reflection;
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
            
            _spriteBatch.DrawString(Rise.Ui.DebugFont, $"Hevadea\nRunning on platform: '{Rise.Platform.GetPlatformName()}'", new Vector2(16, 16), Color.White);
            
            _spriteBatch.End();
        }
    }
}