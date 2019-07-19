using System.Collections.Generic;
using System.Linq;
using Hevadea.Entities.Components.AI;
using Hevadea.Items;
using Hevadea.Registry;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public class ComponentPickup : EntityComponent, IEntityComponentSaveLoad
    {
        public Entity PickedUpEntity { get; private set; }
        public int Offset { get; set; } = 16;

        public void Do()
        {
            var facingTile = Owner.FacingCoordinates;

            if (PickedUpEntity != null) LayDownEntity();
            else PickupEntity(Owner.GetFacingEntity(26));
        }

        public bool HasPickedUpEntity()
        {
            return PickedUpEntity != null;
        }

        public bool PickupEntity(Entity entity)
        {
            if (entity != null)
            {
                if (entity is ItemEntity item && item.Pickup(Owner))
                {
                }
                else if (entity.HasComponent<ComponentPickupable>())
                {
                    PickedUpEntity = entity;
                    entity.Remove();
                    return true;
                }
            }

            return false;
        }

        public bool LayDownEntity()
        {
            var facingTile = Owner.FacingCoordinates;

            if (PickedUpEntity != null && !Owner.Level.QueryEntity(facingTile).Any() &&
                facingTile.InLevelBound(Owner.Level))
            {
                PickedUpEntity.Facing = Owner.Facing;
                Owner.Level.AddEntityAt(PickedUpEntity, facingTile);

                PickedUpEntity.GetComponent<Agent>()?.Abort(AgentAbortReason.PickedUp);

                PickedUpEntity = null;

                return true;
            }

            return false;
        }

        public void OnGameSave(EntityStorage store)
        {
            if (PickedUpEntity != null)
            {
                store.Value("pickup_entity_type", PickedUpEntity.Blueprint.Name);
                store.Value("pickup_entity_data", PickedUpEntity.Save().Data);
            }
        }

        public void OnGameLoad(EntityStorage store)
        {
            var entityType = store.ValueOf("pickup_entity_type", "null");

            if (entityType != "null")
            {
                var entityData = store.ValueOf("pickup_entity_data", new Dictionary<string, object>());
                var entity = ENTITIES.Construct(entityType);
                entity.Load(new EntityStorage(entityType, entityData));

                PickedUpEntity = entity;
            }
        }
    }
}