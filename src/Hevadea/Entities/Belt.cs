using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Entities
{
    public class Belt : Entity
    {
        private static List<Sprite> _sprites;

        public Belt()
        {
            SortingOffset = -16;

            if (_sprites == null)
            {
                _sprites = new List<Sprite>
                {
                    new Sprite(Ressources.TileEntities, new Point(10, 0)),
                    new Sprite(Ressources.TileEntities, new Point(10, 1)),
                    new Sprite(Ressources.TileEntities, new Point(10, 2)),
                    new Sprite(Ressources.TileEntities, new Point(10, 3))
                };
            }

            AddComponent(new Breakable());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.BELT, 1f, 1, 1) } });
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Level
                .GetEntitiesAt(Coordinates)
                .ForEach(x => x.GetComponent<Move>()?.MoveTo(FacingCoordinates, speed: 0.5f));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprites[(int)Facing].Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}