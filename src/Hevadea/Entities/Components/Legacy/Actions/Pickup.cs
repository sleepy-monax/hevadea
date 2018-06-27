using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Items;
using Hevadea.Registry;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Entities.Components.Actions
{
    public class Pickup : EntityComponent, IEntityComponentDrawable, IEntityComponentSaveLoad
    {
        public Entity PickupedEntity { get; private set; }
        public int Offset { get; set; } = 16;

        public void Do()
        {
            var facingTile = Owner.FacingCoordinates;

            if (PickupedEntity != null) LayDownEntity();
            else PickupEntity(Owner.GetFacingEntity(26));
        }

        public bool HasPickedUpEntity()
        {
            return PickupedEntity != null;
        }

        public bool PickupEntity(Entity entity)
        {
            if (entity != null)
            {
                if (entity is ItemEntity item && item.Pickup(Owner))
                {
                }
                else if (entity.HasComponent<Pickupable>())
                {
                    PickupedEntity = entity;
                    entity.Remove();
                    return true;
                }
            }

            return false;
        }

        public bool LayDownEntity()
        {
            var facingTile = Owner.FacingCoordinates;

            if (PickupedEntity != null && !Owner.Level.GetEntitiesAt(facingTile).Any() && facingTile.InLevelBound(Owner.Level))
            {
                PickupedEntity.Facing = Owner.Facing;
                Owner.Level.AddEntityAt(PickupedEntity, facingTile);
                PickupedEntity.GetComponent<Agent>()?.Abort(AgentAbortReason.PickedUp);
                PickupedEntity = null;

                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (PickupedEntity == null) return;

            var sprite = PickupedEntity.GetComponent<Pickupable>().OnPickupSprite;
            sprite.Draw(spriteBatch, new Vector2(Owner.X - sprite.Bound.Width / 2, Owner.Y - sprite.Bound.Width / 2 - Offset), Color.White);
        }

        public void OnGameSave(EntityStorage store)
        {
            if (PickupedEntity != null)
            {
                store.Value("pickup_entity_type", PickupedEntity.Blueprint.Name);
                store.Value("pickup_entity_data", PickupedEntity.Save().Data);
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

                PickupedEntity = entity;
            }
        }
    }
}