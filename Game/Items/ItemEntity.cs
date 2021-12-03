using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Registry;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Items
{
    public class ItemEntity : Entity
    {
        public Item Item { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        public ItemEntity()
        {
            Item = ITEMS.COAL;
            AddComponent(new ComponentMove());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var move = GetComponent<ComponentMove>();
            move.Do(SpeedX, SpeedY);
            SpeedX = SpeedX / 2;
            SpeedY = SpeedY / 2;

            foreach (var e in Level.QueryEntity(Position, Game.Unit))
                if (e.HasComponent<ComponentInventory>(out var i) && i.AlowPickUp)
                {
                    move.MoveTo(e.X, e.Y, 2f);
                    if (Mathf.Distance(e.X, e.Y, X, Y) < 3 && Pickup(e)) return;
                }
        }

        public bool Pickup(Entity entity)
        {
            if (entity?.GetComponent<ComponentInventory>()?.Pickup(Item) ?? false)
            {
                Remove();
                return true;
            }

            return false;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawSprite(Item.Sprite, new Vector2(X + 1 - 3, Y + 1 - 3), 0.50f, Color.Black * 0.10f);
            spriteBatch.DrawSprite(Item.Sprite, new Vector2(X - 3, Y - 3), 0.50f, Color.White);
        }

        public override void OnSave(EntityStorage store)
        {
            store.Value("item", Item.Id);
            store.Value("sx", SpeedX);
            store.Value("sy", SpeedY);
        }

        public override void OnLoad(EntityStorage store)
        {
            Item = ITEMS.ById[store.ValueOf("item", 0)];
            SpeedX = store.ValueOf("sx", SpeedX);
            SpeedY = store.ValueOf("sy", SpeedY);
        }
    }
}