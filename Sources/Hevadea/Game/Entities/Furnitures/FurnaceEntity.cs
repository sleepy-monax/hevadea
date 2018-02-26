using Hevadea.Game.Entities.Component;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Furnitures
{
    public class FurnaceEntity : Entity
    {
        private readonly Sprite _sprite;

        public FurnaceEntity()
        {
            Width = 12;
            Height = 9;
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 1));
            Origin = new Point(8, 6);
            Components.Adds(
                new Breakable(),
                new Light(),
                new Dropable {Items = { new Drop(ITEMS.FURNACE, 1f, 1, 1)}}
            );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 2, (int) Y - 5, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}