using Hevadea.GameObjects.Entities.Components.Ai;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hevadea.Registry;
using System.Linq;

namespace Hevadea.GameObjects.Entities.Components.Actions
{
    public class Pickup : EntityComponent, IEntityComponentDrawable, IEntityComponentSaveLoad
    {
        private Entity _pickupedEntity { get; set; }

        public int Offset { get; set; } = 16;

        public void Do()
        {
            var facingTile = Owner.GetFacingTile();

            if (_pickupedEntity != null) LayDownEntity();
            else PickupEntity(Owner.GetFacingEntity(26));
        }

        public bool HasPickedUpEntity()
        {
            return _pickupedEntity != null;
        }

        public Entity GetPickupEntity()
        {
            return _pickupedEntity;
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
                    _pickupedEntity = entity;
                    entity.Remove();
                    return true;
                }
            }

            return false;
        }

        public bool LayDownEntity()
        {
            var facingTile = Owner.GetFacingTile();

            if (_pickupedEntity != null && !Owner.Level.GetEntitiesAt(facingTile).Any() && facingTile.InLevelBound(Owner.Level))
            {
                _pickupedEntity.Facing = Owner.Facing;
                Owner.Level.AddEntityAt(_pickupedEntity, facingTile.X, facingTile.Y);
                _pickupedEntity.GetComponent<Agent>()?.Abort(AgentAbortReason.PickedUp);
                _pickupedEntity = null;

                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_pickupedEntity == null) return;

            var sprite = _pickupedEntity.GetComponent<Pickupable>().OnPickupSprite;
            sprite.Draw(spriteBatch, new Vector2(Owner.X - sprite.Bound.Width / 2, Owner.Y - sprite.Bound.Width / 2 - Offset), Color.White);
        }

        public void OnGameSave(EntityStorage store)
        {
            if (_pickupedEntity != null)
            {
                store.Value("pickup_entity_type", _pickupedEntity.Blueprint.Name);
                store.Value("pickup_entity_data", _pickupedEntity.Save().Data);
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

                _pickupedEntity = entity;
            }
        }
    }
}