using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Microsoft.Xna.Framework;
using System.IO;

namespace Hevadea.Entities.Components
{
    public static class EntityExtension
    {
        public static bool IsMoving(this Entity entity)
        {
            return entity.GetComponent<Move>()?.IsMoving ?? false;
        }

        public static bool IsSwiming(this Entity entity)
        {
            return entity.HasComponent<Swim>(out var swim) && swim.IsSwiming;
        }
    }
}