using Maker.Hevadea.Game.Items;
using Maker.Rise.Extension;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class Interact : EntityComponent, IDrawableOverlayComponent
    {
        Sprite cursor;

        public Interact()
        {
            cursor = new Sprite(Ressources.tile_icons, 2, new Point(16,16));
        }

        public void Do(Item item)
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
                   
                    if (!e.Components.Has<Interactable>()) continue;
                    e.Components.Get<Interactable>().Interacte(Owner, Owner.Facing, item);
                    break;
                }
            }
        }

        Vector2 Sel = Vector2.Zero;
        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var dir = Owner.Facing.ToPoint();
            var pos = Owner.GetTilePosition();

            var selected = new Point(dir.X + pos.X, dir.Y + pos.Y);
            var selectionRectangle = new Vector2(selected.X * ConstVal.TileSize, selected.Y * ConstVal.TileSize);
            Sel = new Vector2((Sel.X*0.5f + selectionRectangle.X*0.5f), (Sel.Y*0.5f + selectionRectangle.Y * 0.5f));
            var selectedFrame = (gameTime.TotalGameTime.Milliseconds / 100) % 3;
            var value = ((float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 4)) + 1f) / 2;
            cursor.Draw(spriteBatch, Sel + new Vector2( 16f * (1f - value) / 2), value, Color.White);
        }
    }
}
