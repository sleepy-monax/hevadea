using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Items;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Entities.Components
{
    public class ComponentInventory : EntityComponent, IEntityComponentSaveLoad, IEntityComponentOverlay,
        IEntityComponentUpdatable
    {
        private Item _lastAdded;
        private double _addedTimer = 0;
        private float _ix;
        private float _iy;

        private bool _alowPickUp = false;
        public bool HasPickup => _addedTimer > 0.5f;
        public float PickupOffset = 16f;

        public ComponentInventory(int slotCount)
        {
            Content = new ItemStorage(slotCount);
        }

        public ItemStorage Content { get; }

        public bool AlowPickUp
        {
            get => _alowPickUp && Content.HasFreeSlot();
            set => _alowPickUp = value;
        }

        public void OnGameSave(EntityStorage store)
        {
            store.Value(nameof(Content), Content.Items);
        }

        public void OnGameLoad(EntityStorage store)
        {
            var l = store.ValueOf(nameof(Content), new Dictionary<string, object>());
            foreach (var i in l) Content.Items.Add(int.Parse(i.Key), (int) i.Value);
        }

        public bool Pickup(Item item)
        {
            if (AlowPickUp && Content.Add(item))
            {
                _lastAdded = item;
                _addedTimer = 1f;
                _ix = Owner.X;
                _iy = Owner.Y;
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (_addedTimer > 0) _addedTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (_lastAdded != null && _addedTimer <= 0) _lastAdded = null;

            if (_lastAdded != null && _addedTimer >= 0.5f)
            {
                _ix += (Owner.X - _ix) * 0.3f;
                _iy += (Owner.Y - PickupOffset - _iy) * 0.2f;
            }
            else if (_lastAdded != null && _addedTimer < 0.5f)
            {
                _ix += (Owner.X - _ix) * 0.3f;
                _iy += (Owner.Y - _iy) * 0.2f;
            }
        }

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_lastAdded != null)
            {
                var scale = Mathf.Clamp01((float) _addedTimer / 0.5f);
                spriteBatch.DrawSprite(_lastAdded.Sprite, new Vector2(_ix - 8f * scale, _iy - 8f * scale), scale, Color.White);
            }
        }
    }
}