using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;
using Hevadea.GameObjects.Items.Tags;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Hevadea.GameObjects.Entities.Components.Actions
{
    public class Interact : EntityComponent, IEntityComponentDrawableOverlay, IEntityComponentUpdatable
    {
        private Sprite _cursor;
        private Sprite _cursorTile;
        private Sprite _cursorEntity;
        private bool _isEntitySelected = false;

        private float _cursorTileX = 0;
        private float _cursorTileY = 0;

        private float _cursorX = 0;
        private float _cursorY = 0;

        public Interact()
        {
            _cursor = new Sprite(Ressources.TileIcons, 2);
            _cursorEntity = new Sprite(Ressources.TileIcons, new Point(4, 1));
            _cursorTile = new Sprite(Ressources.TileIcons, new Point(5, 1));
        }

        public Coordinates SelectedTile { get; private set; } = new Coordinates(0, 0);

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _cursorEntity.Draw(spriteBatch, new Vector2(_cursorX - 8, _cursorY - 8), Color.White);
            (_isEntitySelected ? _cursorTile : _cursor).Draw(spriteBatch, new Vector2(_cursorTileX - 8, _cursorTileY - 8), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            SelectedTile = Owner.GetFacingTile();
            var selectedEntity = Owner.GetFacingEntity(26);

            float tx = 0;
            float ty = 0;

            float ex = 0;
            float ey = 0;

            tx = (SelectedTile.X * 16 + 8);
            ty = (SelectedTile.Y * 16 + 8);

            if (selectedEntity == null)
            {
                ex = tx;
                ey = ty;
                _isEntitySelected = false;
            }
            else
            {
                ex = (selectedEntity.X);
                ey = (selectedEntity.Y);
                _isEntitySelected = true;
            }

            _cursorTileX += (tx - _cursorTileX) * 0.2f;
            _cursorTileY += (ty - _cursorTileY) * 0.2f;

            _cursorX += (ex - _cursorX) * 0.2f;
            _cursorY += (ey - _cursorY) * 0.2f;
        }

        public void Do(Item item)
        {
            if (!Owner.GetComponent<Pickup>()?.HasPickedUpEntity() ?? true)
            {
                var entities = Owner.GetFacingEntities(26).Where((e) => e.HasComponent<Interactable>());

                if (entities.Any())
                {
                    entities.First().GetComponent<Interactable>().Interacte(Owner, Owner.Facing, item);
                }
                else
                {
                    item?.Tag<InteractItemTag>()?.InteracteOn(Owner, SelectedTile);
                }
            }
        }
    }
}