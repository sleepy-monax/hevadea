using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class TreeEntity : Entity
    {
        private readonly Sprite treeSprite;

        public TreeEntity()
        {
            Width = 4;
            Height = 4;
            Origin = new Point(2, 2);
            treeSprite = new Sprite(Ressources.TileEntities, new Point(7, 0), new Point(1, 3) );

            Components.Adds(
                new Health(5),
                new Dropable {Items = {(ITEMS.WoodLog, 1, 5), (ITEMS.PineCone, 0, 3)}}
            );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var offx = -6;
            var offy = -10;
            treeSprite.Draw(spriteBatch, new Vector2(X + offx, Y + offy - 32), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is PlayerEntity || e is ZombieEntity;
        }
    }
}