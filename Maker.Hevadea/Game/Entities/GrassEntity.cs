using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class GrassEntity : Entity
    {
        private Sprite _sprite;

        public GrassEntity()
        {
            Height = 16;
            Width = 16;

            _sprite = new Sprite(Ressources.TileEntities, new Point(6, 3));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X, Y), Color.White);
        }
    }
}