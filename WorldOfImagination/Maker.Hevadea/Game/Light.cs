using Microsoft.Xna.Framework;
using System;

namespace Maker.Hevadea.Game
{
    [Serializable]
    public class Light
    {
        public bool On { get; set; } = false;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.SpringGreen;
    }
}