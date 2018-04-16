using System.Collections.Generic;
using Hevadea.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components
{
    public class Pushable : EntityComponent, IEntityComponentDrawable
    {
        public Entity IsPushBy;
        public bool CanBePushByAnything => CanBePushBy.Count == 0;
        public List<EntityBlueprint> CanBePushBy { get; set; } = new List<EntityBlueprint>();
        
        public bool Push(Entity pusher, Direction direction, float strength)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;

            if (CanBePushByAnything || CanBePushBy.Contains(pusher.Blueprint))
            {
                var move = Owner.GetComponent<Move>();
                var dir = direction.ToPoint().ToVector2();
                return move?.Do(dir.X * strength, dir.Y * strength, direction) ?? false;
            }
            else
            {
                return false;
            }
        }

        public bool Push(Entity pusher, float sx, float sy)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;

            if (CanBePushByAnything || CanBePushBy.Contains(pusher.Blueprint))
            {
                var move = Owner.GetComponent<Move>();
                var dir = new Vector2(sx, sy);
                return move?.Do(dir.X, dir.Y, dir.ToDirection()) ?? false;
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