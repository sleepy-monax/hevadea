using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Pushable : EntityComponent, IEntityComponentDrawable
    {
        public Entity IsPushBy;
        public bool CanBePushByAnything = false;
        public List<EntityBlueprint> CanBePushBy { get; set; } = new List<EntityBlueprint>();
        
        public bool Push(Entity pusher, Direction direction, float strength)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;

            if (CanBePushByAnything || CanBePushBy.Contains(pusher.Blueprint))
            {
                var move = Owner.Get<Move>();
                var dir = direction.ToPoint().ToVector2();
                return move?.Do(dir.X * strength, dir.Y * strength, direction) ?? false;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            IsPushBy = null;
        }
    }
}