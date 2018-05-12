using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities
{
    public class EntityBelt : Entity
    {
        private static List<Sprite> _sprites;

        public EntityBelt()
        {
            AddComponent(new Breakable());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.BELT, 1f, 1, 1) } });

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
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var entities = Level.GetEntitiesAt(GetTilePosition());
            foreach (var e in entities)
            {
                e.GetComponent<Move>()?.MoveTo(GetFacingTile(), speed: 0.5f);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprites[(int)Facing].Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}