using System;
using Hevadea.Tiles;
using Hevadea.Worlds;

namespace Hevadea.Entities
{
    public static class EntityActions
    {
        public static void Attack(this Entity entity, Entity target)
        {
            // Get the item the entity is holding

            // Compute damages

            // Give damages
        }

        public static void Attack(this Entity entity, Coordinates target)
        {
            // Get the item the entity is holding

            // Compute damages

            // Give damages
        }

        public static void InteractWith(this Entity entity, Entity target)
        {

        }

        public static void InteractWith(this Entity entity, Coordinates target, Level level)
        {

        }

        public static void Ride(this Entity entity, Entity riddable)
        {

        }

        public static void Pickup(this Entity entity, Entity target)
        {

        }

        public static void Move(this Entity entity, float vx, float vy)
        {

        }

        public static void MoveTo(this Entity entity, float x, float y, float speed)
        {

        }

        public static void MoveTo(this Entity entity, Coordinates coords, float speed)
        {

        }
    }
}
