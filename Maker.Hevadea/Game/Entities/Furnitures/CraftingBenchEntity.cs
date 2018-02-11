using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Furnitures
{
    public class CraftingBenchEntity : Entity
    {
        private readonly Sprite _sprite;

        public CraftingBenchEntity()
        {
            Width = 12;
            Height = 9;
            Origin = new Point(8, 6);
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 1));

            Components.Adds(
                new Breakable(),
                new Dropable {Items = {(ITEMS.CraftingbenchItem, 1, 1)}}
            );
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