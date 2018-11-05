using System;
using Hevadea.Tiles;
using Hevadea.Worlds;

namespace Hevadea.Entities
{
    public static class EntityActions
    {
        public static void Attack(this Entity entity, Level level, Entity target)
        {
            // Get the item the entity is holding

            // Compute damages

            // Give damages
        }

        public static void Attack(this Entity entity, Level level, Coordinates target)
        {
            // Get the item the entity is holding

            // Compute damages

            // Give damages
        }

        public static void InteractWith(this Entity entity, Level level, Entity target)
        {

        }

        public static void InteractWith(this Entity entity, Level level, Coordinates target)
        {

        }

        public static void Pickup(this Entity entity, Level level, Entity target)
        {

        }

        public static void Move(this Entity entity, float vx, float vy)
        {

        }

        public static void MoveTo(this Entity entity, float x, float y)
        {

        }
    }
}
