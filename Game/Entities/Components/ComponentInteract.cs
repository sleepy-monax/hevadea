using System.Linq;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Items;
using Hevadea.Items.Tags;
using Hevadea.Systems.InventorySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public class ComponentInteract : EntityComponent, IEntityComponentOverlay, IEntityComponentUpdatable
    {
        private Sprite _cursor;
        private Sprite _cursorTile;
        private Sprite _cursorEntity;
        private bool _isEntitySelected = false;

        private float _cursorTileX = 0;
        private float _cursorTileY = 0;

        private float _cursorX = 0;
        private float _cursorY = 0;

        public ComponentInteract()
        {
            _cursor = new Sprite(Resources.TileIcons, 2);
            _cursorEntity = new Sprite(Resources.TileIcons, new Point(4, 1));
            _cursorTile = new Sprite(Resources.TileIcons, new Point(5, 1));
        }

        public Coordinates SelectedTile { get; private set; } = new Coordinates(0, 0);

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var holdedItem = Owner.HoldedItem();


            if (holdedItem != null)
            {
                spriteBatch.DrawSprite(holdedItem.Sprite, new Vector2(_cursorX - 8, _cursorY - 8), Color.White);
            }
            else
            {
                _cursorEntity.Draw(spriteBatch, new Vector2(_cursorX - 8, _cursorY - 8), Color.White);
            }

            (_isEntitySelected ? _cursorTile : _cursor).Draw(spriteBatch,
                new Vector2(_cursorTileX - 8, _cursorTileY - 8), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            SelectedTile = Owner.FacingCoordinates;
            var selectedEntity = Owner.GetFacingEntity(26);

            var tx = SelectedTile.X * 16 + 8;
            var ty = SelectedTile.Y * 16 + 8;

            float ex;
            float ey;

            if (selectedEntity == null)
            {
                ex = tx;
                ey = ty;
                _isEntitySelected = false;
            }
            else
            {
                ex = selectedEntity.X;
                ey = selectedEntity.Y;
                _isEntitySelected = true;
            }

            _cursorTileX += (tx - _cursorTileX) * 0.2f;
            _cursorTileY += (ty - _cursorTileY) * 0.2f;

            _cursorX += (ex - _cursorX) * 0.2f;
            _cursorY += (ey - _cursorY) * 0.2f;
        }

        public void Do(Item item)
        {
            if (!Owner.GetComponent<ComponentPickup>()?.HasPickedUpEntity() ?? true)
            {
                var entities = Owner.GetFacingEntities(26);

                if (entities.Any())
                {
                    Owner.InteractWith(entities.First(), item);
                }
                else
                {
                    Owner.InteractWith(SelectedTile, item);
                }
            }
        }
    }
}