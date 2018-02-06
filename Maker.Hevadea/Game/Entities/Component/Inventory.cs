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
        public ItemStorage Content { get; private set; }
        public bool AlowPickUp { get; set; } = false;
        
        private Item _lastAdded;
        private readonly EasingManager _pickUpAnimation = new EasingManager();

        public Inventory(int slotCount)
        {
            Content = new ItemStorage(slotCount);
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

        public void Update(GameTime gameTime)
        {
            _pickUpAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_lastAdded != null)
            {
                float size = 1f - _pickUpAnimation.GetValue(EasingFunctions.QuadraticEaseOut);
                _lastAdded.GetSprite().Draw(spriteBatch, new Vector2(Owner.X + Owner.Width / 2f - 8 * size, Owner.Y + Owner.Height / 2 - 24 * size),  size, Color.White);
            }            
        }

        public void OnGameSave(EntityStorage store)
        {
            store.Set(nameof(Content), Content.Items);
        }

        public void OnGameLoad(EntityStorage store)
        {
            Content.Items = store.Get(nameof(Content), Content.Items);
        }
    }
}