using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class TreeEntity : Entity
    {
        Sprite treeSprite;

        public TreeEntity()
        {
            Width = 4;
            Height = 4;

            treeSprite = new Sprite(Ressources.tile_entities, 0, new Point(16,16));
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var offx = -6;
            var offy = -10;
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X + offx, Position.Y + offy - 32), new Point(0, 1), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X + offx, Position.Y + offy - 16), new Point(0, 2), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X + offx, Position.Y + offy - 0), new Point(0, 3), Color.White);
        }
    }
}
