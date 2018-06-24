using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class CraftingBench : Entity
    {
        private readonly Sprite _sprite;

        public CraftingBench()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));

            AddComponent(new Breakable());
            AddComponent(new Burnable(1f));
            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new CraftingStation(RECIPIES.BenchCrafted));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1) } });
            AddComponent(new Move());
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Pushable());
            AddComponent(new Shadow());
            AddComponent(new Physic());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 8, Y - 8), Color.White);
        }
    }
}