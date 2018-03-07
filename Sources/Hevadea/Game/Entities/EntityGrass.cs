using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
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
            Height = 8;
            Width = 8;
            _sprite = new Sprite(Ressources.TileEntities, new Point(6, 3));
            Add(new Breakable());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 4, Y - 4), Color.White);
        }
    }
}