using System;
using Hevadea.Framework;
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
		Sprite _cursorEntity;
		bool _isEntitySelected = false;

		float _cursorX = 0;
		float _cursorY = 0;

        public Interact()
        {
            _cursor = new Sprite(Ressources.TileIcons, 2);
			_cursorEntity = new Sprite(Ressources.TileIcons, new Point(4, 1));
        }

        public TilePosition SelectedTile { get; private set; } = new TilePosition(0, 0);

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
			(_isEntitySelected? _cursorEntity : _cursor).Draw(spriteBatch, new Vector2(_cursorX - 8, _cursorY - 8), _isEntitySelected ? ColorPalette.Accent : Color.White);
        }

        public void Update(GameTime gameTime)
        {
            SelectedTile = Owner.GetFacingTile();
			var selectedEntity = Owner.GetFacingEntity(26);

			float tx = 0;
            float ty = 0;
            
			if (selectedEntity == null)
			{				
                tx = (SelectedTile.X * 16 + 8);
                ty = (SelectedTile.Y * 16 + 8);
				_isEntitySelected = false;
			}
			else
			{
			    tx = (selectedEntity.X);
				ty = (selectedEntity.Y);
				_isEntitySelected = true;
			}
                
			_cursorX += (tx - _cursorX) * 0.1f;
			_cursorY += (ty - _cursorY) * 0.1f;
        }

        public void Do(Item item)
        {
            var entities = Owner.GetFacingEntities(26);
            var asInteracted = false;
            
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

            if (!asInteracted)
            {
                item?.Tag<InteractItemTag>()?.InteracteOn(Owner, SelectedTile);
            }
        }
    }
}