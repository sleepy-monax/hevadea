using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise.Utils
{
    public static class Texture2DUtils
    {

        public static Vector2 GetCenter(this Texture2D tex)
        {
            return tex.Bounds.Center.ToVector2();
        }

    }
}
