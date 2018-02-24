using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Component
{
    public class Light : EntityComponent
    {
        public bool On { get; set; } = false;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.White;
    }
}