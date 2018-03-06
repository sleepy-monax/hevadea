using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Items
{
    public class ItemEntity : Entity
    {
        public Item Item;
        public float sx;
        public float sy;

        public ItemEntity()
        {
            Item = ITEMS.COAL;
            Height = 4;
            Width = 4;
            Origin = new Point(2, 2);

            Add(new Move());
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var move = Get<Move>();
            move.Do(sx, sy, Facing);
            sx = sx / 2;
            sy = sy / 2;
            
            var entities = Level.GetEntitiesOnArea(Bound);
            foreach (var e in entities)
            {
                var inv = e.Get<Inventory>();

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