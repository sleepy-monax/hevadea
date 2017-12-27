using Maker.Rise.GameComponent.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.Game.Entities
{
    public class TreeEntity : Entity
    {
        Sprite treeSprite;

        public TreeEntity()
        {
            Width = 16;
            Height = 16;

            treeSprite = new Sprite(Ressources.tile_entities, 1, new Point(16,16));
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X, Position.Y - 32), new Point(0, 1), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X, Position.Y - 16), new Point(0, 2), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X, Position.Y - 0), new Point(0, 3), Color.White);
        }
    }
}
