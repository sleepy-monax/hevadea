using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class ItemEntity : Entity
    {
        public Item Item;
        private float sx;
        private float sy;

        public ItemEntity() : this(0, 0, 0) { }
        public ItemEntity(Item item) : this(item, 0, 0) { }
        public ItemEntity(int itemId) : this(ITEMS.ById[itemId], 0, 0) { }
        public ItemEntity(int itemId, float speedx, float speedy) : this(ITEMS.ById[itemId], speedx, speedy) {}

        public ItemEntity(Item item, float speedx, float speedy)
        {
            Item = item;
            Height = 4;
            Width = 4;
            Origin = new Point(2, 2);
            sx = speedx;
            sy = speedy;

            Components.Add(new Move());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var move = Components.Get<Move>();
            move.Do(sx, sy, Facing);
            sx = sx / 2;
            sy = sy / 2;

            var magnetEntity = Level.GetEntitiesOnArea(new Rectangle((int)X - 28, (int)Y - 28, 64, 64));

            foreach (var e in magnetEntity)
            {
                var inventory = e.Components.Get<Inventory>();

                if (inventory != null && inventory.AlowPickUp && inventory.Content.HasFreeSlot())
                {
                    var dir = new Vector2(e.X - X, e.Y - Y);
                    var dist = Mathf.Distance(e.X, e.Y, X, Y);

                    dir.Normalize();
                    dir = dir * (1.1f/dist * dist);

                    move.Do(dir.X, dir.Y, Direction.Down);
                }
            }

            var entities = Level.GetEntitiesOnArea(Bound);
            foreach (var e in entities)
            {
                var inv = e.Components.Get<Inventory>();

                if (inv != null && inv.Pickup(Item)) Remove();
            }

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = Item.GetSprite();
            s.Draw(spriteBatch, new Vector2(X + 1 - 3, Y + 1 - 3), 0.50f, Color.Black * 0.10f);
            s.Draw(spriteBatch, new Vector2(X - 3, Y - 3), 0.50f, Color.White);
        }

        public override void OnSave(EntityStorage store)
        {
            store.Set("item", Item.Id);
            store.Set("sx", sx);
            store.Set("sy", sy);
        }

        public override void OnLoad(EntityStorage store)
        {
            Item = ITEMS.ById[store.GetInt("item")];
            sx = store.GetFloat("sx", sx);
            sy = store.GetFloat("sy", sy);
        }
    }
}