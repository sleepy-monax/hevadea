using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityGrass : Entity
    {
        private Sprite _sprite;

        public EntityGrass()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(6, 3));
            Attach(new Breakable());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 8, Y - 8), Color.White);
        }
    }
}