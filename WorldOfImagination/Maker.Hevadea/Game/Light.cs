using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game
{
    public class Light
    {

        public bool On { get; set; } = false;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.SpringGreen;

    }
}
