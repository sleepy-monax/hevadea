using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Pushable : EntityComponent, IEntityComponentDrawable
    {
        public Entity IsPushBy;
        
        public bool Push(Entity pusher, Direction direction, float strength)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;
            var move = Owner.Get<Move>();
            var dir = direction.ToPoint().ToVector2();
            return move?.Do(dir.X * strength, dir.Y * strength, direction) ?? false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            IsPushBy = null;
        }
    }
}