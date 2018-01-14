using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Enum;
using Maker.Rise.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities
{
    public class ItemEntity : Entity
    {
        public readonly Item Item;
        private float sx;
        private float sy;

        public ItemEntity(Item item) : this(item, 0, 0)
        {
        }

        public ItemEntity(Item item, float speedx, float speedy)
        {
            Item = item;
            Height = 4;
            Width = 4;
            Origin = new Point(2, 2);
            sx = speedx;
            sy = speedy;

            AddComponent(new MoveComponent());
        }

        public ItemEntity(int itemId) : this(ITEMS.ById[itemId], 0,0)
        {
        }

        public ItemEntity(int itemId, float speedx, float speedy) : this(ITEMS.ById[itemId], speedx, speedy)
        {

        }

        public override void OnUpdate(GameTime gameTime)
        {
            var entities = Level.GetEntitiesOnArea(Bound);
            GetComponent<MoveComponent>().Move(sx, sy, Facing);
            sx = sx / 2;
            sy = sy / 2;

            foreach (var e in entities)
            {
                if (e.HasComponent<InventoryComponent>())
                {
                    var inv = e.GetComponent<InventoryComponent>();

                    if (inv.Pickup(Item))
                    {
                        Logger.Log<ItemEntity>(LoggerLevel.Info, $"{e.GetType().Name} pickup {Item.GetType().Name}.");
                        Remove();
                    }
                }
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = Item.GetSprite();
            s.Draw(spriteBatch, new Vector2(X + 1 - 3, Y + 1 - 3), 0.50f, Color.Black * 0.10f);
            s.Draw(spriteBatch, new Vector2(X - 3, Y - 3), 0.50f, Color.White);
        }
    }
}