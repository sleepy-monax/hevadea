using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components
{
    public class Light : EntityComponent
    {
        public bool On { get; set; } = false;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.White;
    }
}