using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Extension
{
    public static class Text
    {
        public static string ParseText(string text, SpriteFont font, int width)
        {
            string line = string.Empty;
            string returnString = string.Empty;
            string[] wordArray = text.Split(' ');

            foreach (string word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > width)
                {
                    returnString = returnString + line + '\n';
                    line = string.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }
    }
}