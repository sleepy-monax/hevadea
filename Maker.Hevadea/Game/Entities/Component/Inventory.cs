using System.Linq;
using System;
using System.Collections.Generic;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Storage;
using Maker.Rise.UI;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Inventory : EntityComponent, IDrawableComponent, IUpdatableComponent, ISaveLoadComponent
    {
        private readonly EasingManager _pickUpAnimation = new EasingManager();

        private Item _lastAdded;

        public Inventory(int slotCount)
        {
            Content = new ItemStorage(slotCount);
        }

        public ItemStorage Content { get; }
        public bool AlowPickUp { get; set; } = false;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_lastAdded == null) return;
            var size = 1f - _pickUpAnimation.GetValue(EasingFunctions.QuadraticEaseOut);
            _lastAdded.GetSprite().Draw(spriteBatch,
                new Vector2(Owner.X + Owner.Width / 2f - 8 * size, Owner.Y + Owner.Height / 2 - 24 * size), size,
                Color.White);
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

        
        public void Update(GameTime gameTime)
        {
            _pickUpAnimation.Update(gameTime);
        }

        public bool Pickup(Item item)
        {
            if (AlowPickUp && Content.Add(item))
            {
                _pickUpAnimation.Reset();
                _pickUpAnimation.Show = true;
                _pickUpAnimation.Speed = 0.5f;

                _lastAdded = item;
                return true;
            }

            return false;
        }
    }
}