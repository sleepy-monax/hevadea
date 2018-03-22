using System.Collections.Generic;
using Hevadea.Framework.Utils;
using Hevadea.Game.Storage;
using Hevadea.GameObjects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components
{
    public class Inventory : Component, IEntityComponentSaveLoad
    {
        private Item _lastAdded;
        private double _addedTimer = 0;
        private float _ix;
        private float _iy;

        public bool HasPickup => _addedTimer > 0.5f;
        public float PickupOffset = 16f;
        private bool _alowPickUp = false;

        public Inventory(int slotCount)
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
            store.Set(nameof(Content), Content.Items);
        }

        public void OnGameLoad(EntityStorage store)
        {
            var l = (Dictionary<string,object>)store.Get(nameof(Content), new Dictionary<string,object>());
            foreach (var i in l)
            {
                Content.Items.Add(int.Parse(i.Key), (int)i.Value);
            }
        }

        public bool Pickup(Item item)
        {
            if (AlowPickUp && Content.Add(item))
            {
                _lastAdded = item;
                _addedTimer = 1f;
                _ix = Entity.X;
                _iy = Entity.Y;
                return true;
            }

            return false;
        }

        public override void Update(GameTime gt)
        {
            if (_addedTimer > 0)
            {
                _addedTimer -= gt.ElapsedGameTime.TotalSeconds;
            }

            if (_lastAdded != null && _addedTimer <= 0)
            {
                _lastAdded = null;
            }
            
            if (_lastAdded != null && _addedTimer >= 0.5f)
            {
                _ix += (Entity.X - _ix) * 0.3f;
                _iy += ((Entity.Y - PickupOffset) - _iy) * 0.2f;
            }
            else if (_lastAdded != null && _addedTimer < 0.5f)
            {
                _ix += (Entity.X - _ix) * 0.3f;
                _iy += ((Entity.Y) - _iy) * 0.2f;
            }
        }
        
        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_lastAdded != null)
            {
                var scale = Mathf.Clamp01((float) _addedTimer / 0.5f);
                _lastAdded.GetSprite().Draw(spriteBatch, new Vector2(_ix - 8f * scale, _iy - 8f * scale),  scale,Color.White);
            }
        }
    }
}