using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Utils
{
    public static class XnaUtils
    {

        public static bool IsLegalCharacter(this SpriteFont font, char c)
        {
            return font.Characters.Contains(c) || c == '\r' || c == '\n';
        }

    }
}
