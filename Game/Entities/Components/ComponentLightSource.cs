using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public class ComponentLightSource : EntityComponent
    {
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.White;

        public bool IsOn { get; set; } = false;
        public bool IsOff => !IsOn;
    }
}