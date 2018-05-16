using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.States
{
    public class LightSource : EntityComponent
    {
        public bool IsOn { get; set; } = false;
        public bool IsOff => !IsOn;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.White;
    }
}