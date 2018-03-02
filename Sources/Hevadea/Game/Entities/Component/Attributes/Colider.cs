using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Component.Attributes
{   
    public class Colider : EntityComponent
    {
        public Func<Entity, bool> ColidePredicat = b => { return true;};
        private Rectangle _hitbox;

        public Colider(Rectangle hitbox)
        {
            _hitbox = hitbox;
        }

        public Rectangle GetHitBox()
        {
            return new Rectangle((int)Owner.X + _hitbox.X, (int)Owner.Y + _hitbox.Y, _hitbox.Width, _hitbox.Height);
        }

        public List<Entity> GetTouchingEntities()
        {
            return Owner.Level.GetEntitiesOnArea(GetHitBox());
        }

        public bool IsColiding(Entity e)
        {
            var colider = e.Get<Colider>();
            return e != Owner && colider != null && colider.GetHitBox().Contains(GetHitBox()) && ColidePredicat(e);
        }
    }
}