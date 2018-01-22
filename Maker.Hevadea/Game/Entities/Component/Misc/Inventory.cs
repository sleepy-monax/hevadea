using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Storage;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class Inventory : EntityComponent, IDrawableComponent, IUpdatableComponent, ISaveLoadComponent
    {
        public ItemStorage Content { get; private set; }
        public bool AlowPickUp { get; set; } = false;
        private Item lastAdded;
        private readonly FadingAnimation anim = new FadingAnimation();

        public Inventory(int slotCount)
        {
            Content = new ItemStorage(slotCount);
        }
        
        public bool Pickup(Item item)
        {
            if (AlowPickUp && Content.Add(item))
            {
                anim.Reset();
                anim.Show = true;
                anim.Speed = 0.50f;

                lastAdded = item;
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            anim.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (lastAdded != null)
            {
                float size = 1f - anim.SinTwoPhases;
                lastAdded.GetSprite().Draw(spriteBatch, new Vector2(Owner.X + Owner.Width / 2f - 8 * size, Owner.Y + Owner.Height / 2 - 24 * size),  size, Color.White);
            }            
        }

        public void OnSave(EntityStorage store)
        {
            store.Set(nameof(Content), Content.Items);
        }

        public void OnLoad(EntityStorage store)
        {
            Content.Items = store.Get(nameof(Content), Content.Items);
        }
    }
}