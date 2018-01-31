using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
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
            sprite = new Sprite(Ressources.tile_entities, new Point(2, 3));
            
            Components.Adds(
                new Breakable(),
                new Dropable(),
                new Light()
                );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, new Rectangle((int)X - 2, (int)Y - 5, 16, 16), Color.White);
        }
    }
}