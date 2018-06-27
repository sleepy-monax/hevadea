using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components.Attributes
{
    public class Physic : EntityComponent
    {
        public float Mass { get; set; } = 1f;
        public Vector2 Velocity { get; set; } = new Vector2();
        public Vector2 Acceleration { get; set; } = new Vector2();
    }
}