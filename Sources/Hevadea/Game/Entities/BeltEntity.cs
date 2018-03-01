using Hevadea.Framework.Graphic;
using Hevadea.Game.Entities.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class BeltEntity : Entity
    {
        public BeltEntity()
        {
            Height = 8;
            Width = 8;
        }
        
        public override void OnUpdate(GameTime gameTime)
        {
            var entity = Level.GetEntityOnTile(GetTilePosition());
            var dir = Facing.ToPoint();
            
            foreach (var e in entity)
            {
                e.Components.Get<Move>()?.Do(dir.X, dir.Y, Facing);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
               spriteBatch.FillRectangle(Bound, Color.Black);
        }
    }
}