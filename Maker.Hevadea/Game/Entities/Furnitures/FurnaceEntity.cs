using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class FurnaceEntity : Entity
    {
        private readonly Sprite sprite;
        
        public FurnaceEntity()
        {
            Width = 12;
            Height = 9;
            sprite = new Sprite(Ressources.tile_entities, new Point(2, 3));
            Origin = new Point(8, 6);
            Components.Adds(
                new Breakable(),
                new Light(),
                new Dropable() { Items = { (ITEMS.FurnaceItem, 1,1)} }
                );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, new Rectangle((int)X - 2, (int)Y - 5, 16, 16), Color.White);
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}