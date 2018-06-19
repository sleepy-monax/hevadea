using Hevadea.Framework.Utils;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Hevadea.Registry;

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
            AddComponent(new Move());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var move = GetComponent<Move>();
            move.Do(SpeedX, SpeedY);
            SpeedX = SpeedX / 2;
            SpeedY = SpeedY / 2;

            var entities = Level.GetEntitiesOnArea(new Rectangle((int)X - 8, (int)Y - 8, 16, 16))
                                .Where((e) => e.GetComponent<Inventory>()?.AlowPickUp ?? false);

            foreach (var e in entities)
            {
                move.MoveTo(e.X, e.Y);
                if (Mathf.Distance(e.X, e.Y, X, Y) < 3 && Pickup(e)) return;
            }
        }

        public bool Pickup(Entity entity)
        {
            if (entity?.GetComponent<Inventory>()?.Pickup(Item) ?? false)
            {
                Remove();
                return true;
            }
            return false;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = Item.GetSprite();
            s.Draw(spriteBatch, new Vector2(X + 1 - 3, Y + 1 - 3), 0.50f, Color.Black * 0.10f);
            s.Draw(spriteBatch, new Vector2(X - 3, Y - 3), 0.50f, Color.White);
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