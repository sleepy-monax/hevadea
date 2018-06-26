using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Extension
{
    public static class Vector2Extention
    {

        public static Vector2 GetXVector2(this Vector2 vector)
        {
            return new Vector2(vector.X, 0);
        }

        public static Vector2 GetYVector2(this Vector2 vector)
        {
            return new Vector2(0, vector.Y);
        }

    }
}
