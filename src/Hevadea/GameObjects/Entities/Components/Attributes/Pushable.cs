using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Pushable : EntityComponent, IEntityComponentDrawable
    {
        public Entity IsPushBy { get; private set; }
        public List<EntityBlueprint> CanBePushBy { get; set; } = new List<EntityBlueprint>();
        public bool CanBePushByAnything => CanBePushBy.Count == 0;

        public bool Push(Entity pusher, Direction direction, float strength)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;

            if (CanBePushByAnything || CanBePushBy.Contains(pusher.Blueprint))
            {
                var move = Owner.GetComponent<Move>();
                var dir = direction.ToPoint().ToVector2();
                return move?.Do(dir.X * strength, dir.Y * strength) ?? false;
            }

            return false;
        }

        public bool Push(Entity pusher, float sx, float sy)
        {
            if (IsPushBy != null) return false;
            IsPushBy = pusher;

            if (CanBePushByAnything || CanBePushBy.Contains(pusher.Blueprint))
            {
                var move = Owner.GetComponent<Move>();
                var dir = new Vector2(sx, sy);
                return move?.Do(dir.X, dir.Y) ?? false;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            IsPushBy = null;
        }
    }
}