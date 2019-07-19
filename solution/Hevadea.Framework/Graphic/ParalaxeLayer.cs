using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Graphic
{
    public class ParalaxeLayer
    {
        public Texture2D Texture { get; }
        public double Factor { get; }

        public ParalaxeLayer(Texture2D texture, double factor)
        {
            Texture = texture;
            Factor = factor;
        }
    }
}
