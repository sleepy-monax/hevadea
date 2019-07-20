using Hevadea.Items;
using Hevadea.Items.Tags;
using Microsoft.Xna.Framework;
using System.IO;

namespace Hevadea.Entities.Components
{
    public static class EntityExtension
    {
        public static void Pickup(this Entity entity, Entity target)
        {
            entity.GetComponent<ComponentPickup>()?.PickupEntity(target);
        }

        public static void Ride(this Entity entity, Entity ride)
        {
            if (ride.HasComponent<ComponentRideable>() && ride.GetComponent<ComponentRideable>().Rider == null)
            {
                ride.GetComponent<ComponentRideable>().Rider = entity;
                entity.Remove();
            }
        }

        public static void InteractWith(this Entity entity, Entity target, Item item = null)
        {
            if (!entity.IsHolding())
            {
                if (target.HasComponent<ComponentInteractive>(out var interactable))
                {
                    interactable.Interacte(entity, entity.Facing, item);
                }
                else if (target.HasComponent<ComponentRideable>())
                {
                    entity.Ride(target);
                }
            }
        }

        public static void InteractWith(this Entity entity, Coordinates coords, Item item = null)
        {
            item?.Tag<InteractItemTag>()?.InteracteOn(entity, coords);
        }

        public static bool IsMoving(this Entity entity)
        {
            return entity.GetComponent<ComponentMove>()?.IsMoving ?? false;
        }

        public static bool IsSwimming(this Entity entity)
        {
            return entity.GetComponent<ComponentSwim>()?.IsSwiming ?? false;
        }

        public static bool IsHolding(this Entity entity)
        {
            return entity.GetComponent<ComponentPickup>()?.PickedUpEntity != null ||
                   entity.GetComponent<ComponentRideable>()?.Rider != null ||
                   (entity.GetComponent<ComponentInventory>()?.HasPickup ?? false);
        }

        public static Entity GetHoldedEntity(this Entity entity)
        {
            if (entity.HasComponent<ComponentPickup>())
            {
                return entity.GetComponent<ComponentPickup>().PickedUpEntity;
            }
            else if (entity.HasComponent<ComponentRideable>())
            {
                return entity.GetComponent<ComponentRideable>().Rider;
            }
            else
            {
                return null;
            }
        }
    }
}