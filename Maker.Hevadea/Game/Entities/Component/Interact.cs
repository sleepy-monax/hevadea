using System;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Interact : EntityComponent, IDrawableOverlayComponent, IUpdatableComponent
    {
        public TilePosition SelectedTile { get; private set; } = new TilePosition(0, 0);

        Sprite cursor;
        Vector2 selectionCursorPosition = Vector2.Zero;

        public Interact()
        {
            cursor = new Sprite(Ressources.tile_icons, 2, new Point(16,16));
        }

        public void Do(Item item)
        {
            var entities = Owner.Level.GetEntityOnTile(SelectedTile);

            if (entities.Count > 0)
            {
                foreach (var e in entities)
                {
                    var interactable = e.Components.Get<Interactable>();

                    if (interactable != null)
                    {
                        interactable.Interacte(Owner, Owner.Facing, item);
                        break;
                    }
                }
            }
            else if (item != null)
            {
                item.InteracteOn(Owner, SelectedTile);
            }
        }

        public void Update(GameTime gameTime)
        {

            SelectedTile = Owner.GetFacingTile();
        }
        
        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var selectionRectangle = new Vector2(SelectedTile.X * ConstVal.TileSize, SelectedTile.Y * ConstVal.TileSize);
            selectionCursorPosition = new Vector2((selectionCursorPosition.X*0.8f + selectionRectangle.X*0.2f), (selectionCursorPosition.Y*0.8f + selectionRectangle.Y * 0.2f));

            var animation = ((float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 4)) + 1f) / 2;
            cursor.Draw(spriteBatch, selectionCursorPosition + new Vector2( 16f * (1f - animation) / 2), animation, Color.White);
        }

    }
}
