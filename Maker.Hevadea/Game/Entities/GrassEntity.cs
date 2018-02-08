using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class GrassEntity : Entity
    {
        private readonly Sprite Sprite;

        public GrassEntity()
        {
            Height = 16;
            Width = 16;

            Sprite = new Sprite(Ressources.TileEntities, new Point(1, 2));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, new Vector2(X, Y), Color.White);
        }
    }
}