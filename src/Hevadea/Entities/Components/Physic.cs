using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public class Physic : EntityComponent
    {
        public float Mass { get; set; } = 1f;
        public float Speed => Velocity.Length();
        public Vector2 Velocity { get; set; } = new Vector2();
        public Vector2 Acceleration { get; set; } = new Vector2();

        public void Stop()
        {
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
        }

        public void AddVelocityButCapSpeed(Vector2 velocity, float maxSpeed)
        {

            var newVelocity = Velocity + velocity;

            if (newVelocity.Length() > maxSpeed)
            {
                newVelocity.Normalize();
                newVelocity = newVelocity * maxSpeed;
            }

            Velocity = newVelocity;
        }
    }
}