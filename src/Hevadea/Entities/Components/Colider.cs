using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hevadea.Entities.Components
{
    public class Colider : EntityComponent, IEntityComponentOverlay
    {
        public Func<Entity, bool> ColidePredicat = b => { return true; };
        RectangleF _hitbox;

        public Colider(Rectangle hitbox)
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
            return ColidePredicat(e);
        }

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Rise.Debug.GAME)
                spriteBatch.DrawRectangle(GetHitBox(), Color.Red, 1 / Owner.GameState.Camera.Zoom);
        }
    }
}