using Maker.Hevadea.Game.Items;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class InteractComponent : EntityComponent, IDrawableOverlayComponent
    {
        public void Interact(Item item)
        {
            var tilePosition = Owner.GetTilePosition();
            var dir = Owner.Facing.ToPoint();


            var entities = Owner.Level.GetEntitiesOnArea(new Rectangle((int)(Owner.X + Owner.Height * dir.X),
                (int)(Owner.Y + Owner.Width * dir.Y),
                Owner.Height, Owner.Width));

            if (entities.Count > 0)
            {
                foreach (var e in entities)
                {
                   
                    if (!e.HasComponent<InteractableComponent>()) continue;
                    e.GetComponent<InteractableComponent>().Interacte(Owner, Owner.Facing, item);
                    break;
                }
            }
        }
      
        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var dir = Owner.Facing.ToPoint();


            var entities = Owner.Level.GetEntitiesOnArea(new Rectangle((int)(Owner.X + Owner.Height * dir.X),
                (int)(Owner.Y + Owner.Width * dir.Y),
                Owner.Height, Owner.Width));

            if (entities.Count > 0)
            {
                foreach (var e in entities)
                {
                    if (e.HasComponent<InteractableComponent>())
                    {
                        spriteBatch.FillRectangle(e.Bound, Color.Red * (float)((Math.Sin(gameTime.TotalGameTime.TotalSeconds * 4f) + 1f) / 2f));
                    }
                    break;
                }
            }
        }
    }
}
