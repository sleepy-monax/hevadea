using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Registry;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Hevadea.GameObjects.Items;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public class EntityCraftingBench : Entity
    {
        private readonly Sprite _sprite;

        public EntityCraftingBench()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));

            Attach(new Breakable());
            Attach(new Dropable {Items = {new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1)}});
            Attach(new Pushable {CanBePushByAnything = true});
            Attach(new Move());
            Attach(new Colider(new Rectangle(-6, -2, 12, 8)));
            Attach(new Burnable(1f));
            Attach(new Interactable()).Interacted += OnInteracted;
            Attach(new Pickupable(_sprite));
        }

        private void OnInteracted(object sander, InteractEventArg e)
        {
            if (e.Entity.HasComponent<Inventory>())
                Game.CurrentMenu = new MenuPlayerInventory(e.Entity, RECIPIES.BenchCrafted, Game);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}