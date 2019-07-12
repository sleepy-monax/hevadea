using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Entities.Components
{
    public class Collider : EntityComponent, IEntityComponentOverlay
    {
        public Func<Entity, bool> CollidePredicat = b => true;
        readonly RectangleF _hitbox;

        public Collider(Rectangle hitbox)
        {
            _hitbox = hitbox;
        }

        public RectangleF GetHitBox()
        {
            return new RectangleF(Owner.X + _hitbox.X, Owner.Y + _hitbox.Y, _hitbox.Width, _hitbox.Height);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)(Owner.X + _hitbox.X), (int)(Owner.Y + _hitbox.Y), (int)_hitbox.Width, (int)_hitbox.Height);
        }

        public bool CanCollideWith(Entity e)
        {
            return CollidePredicat(e);
        }

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Rise.Debug.GAME)
                spriteBatch.DrawRectangle(GetHitBox(), Color.Red, 1 / Owner.GameState.Camera.Zoom);
        }
    }
}