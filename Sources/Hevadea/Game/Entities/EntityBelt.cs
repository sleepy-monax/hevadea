using System.Collections.Generic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityBelt : Entity
    {
        private static List<Sprite> _sprites;
        
        public EntityBelt()
        {
            Height = 16;
            Width = 16;

            Add(new Breakable());
            Add(new Dropable{ Items = { new Drop(ITEMS.BELT, 1f, 1, 1) } });

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
            var entities = Level.GetEntityOnTile(GetTilePosition());
            foreach (var e in entities)
            {
                e.Get<Pushable>()?.Push(this, Facing, 0.5f);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {            
            _sprites[(int)Facing].Draw(spriteBatch, Bound, Color.White);
        }
    }
}