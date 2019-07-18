using Microsoft.Xna.Framework;
using System.IO;

namespace Hevadea.Entities.Components
{
    public static class EntityExtension
    {
        public static bool IsMoving(this Entity entity)
        {
            return entity.GetComponent<ComponentMove>()?.IsMoving ?? false;
        }

        public static bool IsSwimming(this Entity entity)
        {
            return entity.GetComponent<ComponentSwim>()?.IsSwiming ?? false;
        }
    }
}