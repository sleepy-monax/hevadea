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

        public static bool IsLifting(this Entity entity)
        {
            return entity.GetComponent<ComponentPickup>()?.PickedUpEntity != null;
        }

        public static Entity GetPickedUpEntity(this Entity entity)
        {
            if (entity.HasComponent<ComponentPickup>())
            {
                return entity.GetComponent<ComponentPickup>().PickedUpEntity;
            }
            else
            {
                return null;
            }
        }
    }
}