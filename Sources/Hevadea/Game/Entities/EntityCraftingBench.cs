using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityCraftingBench : Entity
    {
        private readonly Sprite _sprite;

        public EntityCraftingBench()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));

            Attachs(
                new Breakable(),
                new Pushable(),
                new Move(),
                new Dropable {Items = {new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1)}}
            );
            
            Attach(new Interactable()).OnInteracte +=
                (sender, arg) =>
                {
                    if (arg.Entity.Has<Inventory>())
                        Game.CurrentMenu = new MenuPlayerInventory(arg.Entity, RECIPIES.BenchCrafted, Game);
                };
            Attach(new Colider(new Rectangle(-8, 0, 16, 8)));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is EntityPlayer || e is EntityZombie;
        }
    }
}