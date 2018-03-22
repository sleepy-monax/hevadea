using System.Collections.Generic;
using Hevadea.Game;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Pushable : Component
    {
        public Entity IsPushBy;
        public bool CanBePushByAnything = false;
        public List<EntityBlueprint> CanBePushBy { get; set; } = new List<EntityBlueprint>();
        
        public bool Push(Entity pusher, Direction direction, float strength)
        {
            if (IsPushBy != null) return false;

            if ((CanBePushByAnything ||
                 CanBePushBy.Contains(pusher.Blueprint)) &&
                 Entity.GetComponent<Move>(out var move))
            {
                IsPushBy = pusher;
                var dir = direction.ToPoint().ToVector2();
                return move.Do(dir.X * strength, dir.Y * strength, direction);
            }

            return false;
        }

        public bool Push(Entity pusher, float sx, float sy)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;

            if (CanBePushByAnything || CanBePushBy.Contains(pusher.Blueprint))
            {
                var move = Entity.GetComponent<Move>();
                var dir = new Vector2(sx, sy);
                return move?.Do(dir.X, dir.Y, dir.ToDirection()) ?? false;
            }
            else
            {
                return false;
            }
        }

        public override void Draw(SpriteBatch sb, GameTime gt)
        {
            IsPushBy = null;
        }
    }
}