using System.Collections.Generic;
using Hevadea.Entities.Components.Ai;
using Hevadea.Registry;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public class Pickup : Component, IEntityComponentDrawable, IEntityComponentSaveLoad
    {
        private Entity _pickupedEntity { get; set; } 
        
        public int Offset { get; set; } = 16;
                     
        public void Do()
        {
            var facingTile = Owner.GetFacingTile();
            
            if (_pickupedEntity != null) LayDownEntity();
            else foreach (var e in Owner.Level.GetEntityOnTile(facingTile))
                if (PickupEntity(e)) return;                     
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
            if (!entity.HasComponent<Pickupable>()) return false;
            _pickupedEntity = entity;
            entity.Remove();
            return true;
        }

        public bool LayDownEntity()
        {
            var facingTile = Owner.GetFacingTile();
            
            if (Owner.Level.GetEntityOnTile(facingTile).Count != 0) return false;
            
            _pickupedEntity.Facing = Owner.Facing;
            Owner.Level.SpawnEntity(_pickupedEntity, facingTile.X, facingTile.Y);
            _pickupedEntity.GetComponent<Agent>()?.Abort(AgentAbortReason.PickedUp);
            _pickupedEntity = null;

            return true;
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
                store.Set("pickup_entity_type", _pickupedEntity.Blueprint.Name);
                store.Set("pickup_entity_data", _pickupedEntity.Save().Data);
            }
        }

        public void OnGameLoad(EntityStorage store)
        {
            var entityType = (string)store.Get("pickup_entity_type", "null");

            if (entityType != "null")
            {
                var entityData = (Dictionary<string, object>)store.Get("pickup_entity_data", new Dictionary<string, object>());
                var entityBlueprint = ENTITIES.GetBlueprint(entityType);
                var entity = entityBlueprint.Construct();
                entity.Load(new EntityStorage(entityType, entityData));

                _pickupedEntity = entity;
            }
        }
    }
}