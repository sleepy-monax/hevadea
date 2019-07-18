using Microsoft.Xna.Framework;

namespace Hevadea.Framework.Extension
{
    public static class GameTimeExtension
    {
        public static float GetDeltaTime(this GameTime gameTime)
        {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static float GetTotalTime(this GameTime gameTime)
        {
            return (float) gameTime.TotalGameTime.TotalSeconds;
        }
    }
}