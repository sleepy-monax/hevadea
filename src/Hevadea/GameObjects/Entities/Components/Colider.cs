using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities.Components
{
    public class Colider : EntityComponent, IEntityComponentDrawableOverlay
    {
        public Func<Entity, bool> ColidePredicat = b => { return true; };
        private RectangleF _hitbox;

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

        public List<Entity> GetTouchingEntities()
        {
            return Owner.Level.GetEntitiesOnArea(ToRectangle());
        }

        public bool CanCollideWith(Entity e)
        {
            return ColidePredicat(e);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Rise.DebugUi)
                spriteBatch.DrawRectangle(ToRectangle(), Color.Red, 1 / Owner.GameState.Camera.Zoom);
        }
    }
}