using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Extension
{
    public static class SpriteFontExtension
    {
        public static bool IsLegalCharacter(this SpriteFont font, char c)
        {
            return font.Characters.Contains(c) || c == '\r' || c == '\n';
        }
    }
}
