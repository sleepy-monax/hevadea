using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Items;
using Hevadea.Game.Items.Tags;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Game.Entities.Components.Interaction
{
    public class Interact : EntityComponent, IEntityComponentDrawableOverlay, IEntityComponentUpdatable
    {
        private readonly Sprite _cursor;
        private Vector2 _selectionCursorPosition = Vector2.Zero;

        public Interact()
        {
            _cursor = new Sprite(Ressources.TileIcons, 2, new Point(16, 16));
        }

        public TilePosition SelectedTile { get; private set; } = new TilePosition(0, 0);

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var selectionRectangle =
                new Vector2(SelectedTile.X * ConstVal.TileSize, SelectedTile.Y * ConstVal.TileSize);
            _selectionCursorPosition = new Vector2(_selectionCursorPosition.X * 0.8f + selectionRectangle.X * 0.2f,
                _selectionCursorPosition.Y * 0.8f + selectionRectangle.Y * 0.2f);

            var animation = ((float) Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 4)) + 1f) / 2;
            _cursor.Draw(spriteBatch, _selectionCursorPosition + new Vector2(16f * (1f - animation) / 2), animation,
                Color.White);
        }

        public void Update(GameTime gameTime)
        {
            SelectedTile = Owner.GetFacingTile();
        }

        public void Do(Item item)
        {
            var entities = Owner.Level.GetEntityOnTile(SelectedTile);
            var asInteracted = false;
            if (entities.Count > 0)
            {

                foreach (var e in entities)
                {
                    var interactable = e.Get<Interactable>();

                    if (interactable != null)
                    {
                        interactable.Interacte(Owner, Owner.Facing, item);
                        asInteracted = true;
                        break;
                    }
                }
            }
            if (!asInteracted)
            {
                item?.Tag<InteractItemTag>()?.InteracteOn(Owner, SelectedTile);
            }
        }
    }
}