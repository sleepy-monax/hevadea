using System.Collections.Generic;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Pickup : EntityComponent, IEntityComponentDrawable, IEntityComponentSaveLoad
    {
        public int Offset { get; set; } = 16;
        public Entity PickupEntity { get; set; } 
             
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (PickupEntity != null)
            {
                var sprite = PickupEntity.Get<Pickupable>().OnPickupSprite;
                sprite.Draw(spriteBatch, new Vector2(AttachedEntity.X - sprite.Bound.Width / 2, AttachedEntity.Y - sprite.Bound.Width / 2 - Offset), Color.White);
            }
        }

        public void Do()
        {
            var facingTile = AttachedEntity.GetFacingTile();
            
            if (PickupEntity != null)
            {
                if (AttachedEntity.Level.GetEntityOnTile(facingTile).Count == 0)
                {
                    PickupEntity.Facing = AttachedEntity.Facing;
                    AttachedEntity.Level.SpawnEntity(PickupEntity, facingTile.X, facingTile.Y);
                    PickupEntity = null;
                }
            }
            else
            { 
                var entities = AttachedEntity.Level.GetEntityOnTile(facingTile);
    
                foreach (var e in entities)
                {
                    if (e.Has<Pickupable>())
                    {
                        PickupEntity = e;
                        e.Remove();
                        return;
                    }
                }
            }
        }

        public void OnGameSave(EntityStorage store)
        {
            var pickedEntityData = PickupEntity.Save();
            
            store.Set("pickup_entity_type", pickedEntityData.Type);
            store.Set("pickup_entity_data", pickedEntityData.Data);
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

                PickupEntity = entity;
            }
        }
    }
}