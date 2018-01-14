using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class LightComponent : EntityComponent
    {
        public bool On { get; set; } = false;
        public int Power { get; set; } = 32;
        public Color Color { get; set; } = Color.White;
    }
}
