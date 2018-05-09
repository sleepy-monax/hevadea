using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;
using Hevadea.GameObjects.Items.Tags;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Actions
{
    public class Interact : EntityComponent, IEntityComponentDrawableOverlay, IEntityComponentUpdatable
    {
        Sprite _cursor;
        Sprite _cursorTile;
        Sprite _cursorEntity;
        bool _isEntitySelected = false;

        float _cursorTileX = 0;
        float _cursorTileY = 0;

        float _cursorX = 0;
        float _cursorY = 0;

        public Interact()
        {
            _cursor = new Sprite(Ressources.TileIcons, 2);
            _cursorEntity = new Sprite(Ressources.TileIcons, new Point(4, 1));
            _cursorTile = new Sprite(Ressources.TileIcons, new Point(5, 1));
        }

        public TilePosition SelectedTile { get; private set; } = new TilePosition(0, 0);

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            (_isEntitySelected ? _cursorTile : _cursor).Draw(spriteBatch, new Vector2(_cursorTileX - 8, _cursorTileY - 8), Color.White);

            if (_isEntitySelected)
            {
                _cursorEntity.Draw(spriteBatch, new Vector2(_cursorX - 8, _cursorY - 8), Color.White);
            }
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

            _cursorTileX += (tx - _cursorTileX) * 0.1f;
            _cursorTileY += (ty - _cursorTileY) * 0.1f;


            _cursorX += (ex - _cursorX) * 0.5f;
            _cursorY += (ey - _cursorY) * 0.5f;
            


        }

        public void Do(Item item)
        {
            var entities = Owner.GetFacingEntities(26);
            
			var asInteracted = item?.Tag<InteractItemTag>()?.InteracteOn(Owner, SelectedTile) ?? false;
           
            if (!asInteracted)
            {
				if (!Owner.GetComponent<Pickup>()?.HasPickedUpEntity() ?? true && entities.Count > 0)
                {

                    foreach (var e in entities)
                    {
                        var interactable = e.GetComponent<Interactable>();

                        if (interactable != null)
                        {
                            interactable.Interacte(Owner, Owner.Facing, item);
                            asInteracted = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}