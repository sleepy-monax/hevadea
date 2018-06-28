using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public static class EntityExtension
    {
        public static bool IsMoving(this Entity entity)
        {
            return (entity.HasComponent<Physic>(out var physic) && physic.Velocity != Vector2.Zero) || (entity.HasComponent<Move>(out var move) && move.IsMoving);
        }

        public static bool IsSwiming(this Entity entity)
        {
            return entity.HasComponent<Swim>(out var swim) && swim.IsSwiming;
        }

        public static bool IsGrounded(this Entity entity)
        {
            return entity.Z <= 0f && !entity.IsSwiming();
        }
    }
}