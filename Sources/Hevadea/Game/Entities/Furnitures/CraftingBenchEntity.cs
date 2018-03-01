using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Furnitures
{
    public class CraftingBenchEntity : Entity
    {
        private readonly Sprite _sprite;

        public CraftingBenchEntity()
        {
            Width = 12;
            Height = 9;
            Origin = new Point(8, 6);
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));

            Components.Adds(
                new Breakable(),
                new Dropable {Items = {new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1)}}
            );
            
            Components.Add(new Interactable()).OnInteracte +=
                (sender, arg) =>
                {
                    if (arg.Entity.Components.Has<Inventory>())
                        Game.CurrentMenu = new InventoryMenu(arg.Entity, RECIPIES.BenchCrafted, Game);
                };
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 2, (int) Y - 5, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is PlayerEntity || e is ZombieEntity;
        }
    }
}