using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Blueprints
{
    public class EntityGrass : Entity
    {
        private Sprite _sprite;

        public EntityGrass()
        {
            SortingOffset = -16;
            _sprite = new Sprite(Ressources.TileEntities, new Point(6, 3));
            AddComponent(new Breakable());
            AddComponent(new Burnable(1f));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 8, Y - 8), Color.White);
        }
    }
}