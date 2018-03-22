using System;
using System.Collections.Generic;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Colider : Component
    {
        private RectangleF _hitbox;
        public Func<Entity, bool> ColidePredicat = b => { return true;};

        public Colider(Rectangle hitbox)
        {
            _hitbox = hitbox;
        }

        public RectangleF GetHitBox()
        {
            return new RectangleF(Entity.X + _hitbox.X, Entity.Y + _hitbox.Y, _hitbox.Width, _hitbox.Height);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)(Entity.X + _hitbox.X), (int)(Entity.Y + _hitbox.Y), (int)_hitbox.Width, (int)_hitbox.Height);
        }

        public List<Entity> GetTouchingEntities()
        {
            return Entity.Level.GetEntitiesOnArea(ToRectangle());
        }

        public bool CanCollideWith(Entity e)
        {
            return ColidePredicat(e);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Rise.DebugUI)
                spriteBatch.DrawRectangle(ToRectangle(), Color.Red, 1 / Entity.Game.Camera.Zoom);
        }
    }
}