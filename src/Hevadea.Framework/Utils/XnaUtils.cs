using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Utils
{
    public static class XnaUtils
    {

        public static bool IsLegalCharacter(this SpriteFont font, char c)
        {
            return font.Characters.Contains(c) || c == '\r' || c == '\n';
        }

        public static float GetDeltaTime(this GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static float GetTotalTime(this GameTime gameTime)
        {
            return (float)gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
