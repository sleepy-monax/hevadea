using Maker.Hevadea.Game.Items;
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

            treeSprite = new Sprite(Ressources.tile_entities, 0, new Point(16, 16));
            IsInvincible = false;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var offx = -6;
            var offy = -10;

            treeSprite.DrawSubSprite(spriteBatch, new Vector2(X + offx, Y + offy - 32), new Point(0, 1), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(X + offx, Y + offy - 16), new Point(0, 2), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(X + offx, Y + offy - 0), new Point(0, 3), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            if (e is Player)
            {
                return true;
            }

            return false;
        }

        public override void Die()
        {
            var dropWood = new ItemEntity(new WoodLogItem());
            Level.AddEntity(dropWood);
            dropWood.MoveTo(X, Y);
            base.Die();
        }
    }
}