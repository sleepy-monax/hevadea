using Maker.Hevadea.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities
{
    public class ItemEntity : Entity
    {
        Item Item;

        public ItemEntity(Item item)
        {
            Item = item;
            Height = 4;
            Width = 4;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var entities = Level.GetEntitiesOnArea(Bound);

            foreach (var e in entities)
            {
                if (e is Mob m && m.Pickup(Item))
                {
                    Console.WriteLine($"{e.GetType().Name} pickup {Item.GetType().Name}");
                    Remove();
                }
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Item.Sprite.Draw(spriteBatch, new Vector2(X + 1 - 3, Y + 1 - 3), 0.50f, Color.Black * 0.50f);
            Item.Sprite.Draw(spriteBatch, new Vector2(X - 3, Y - 3), 0.50f, Color.White);
        }
    }
}