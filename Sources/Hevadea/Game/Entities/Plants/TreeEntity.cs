using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Plants
{
    public class TreeEntity : Entity
    {
        private readonly Sprite treeSprite;

        public TreeEntity()
        {
            Width = 4;
            Height = 4;
            Origin = new Point(2, 2);
            treeSprite = new Sprite(Ressources.TileEntities, new Point(6, 4), new Point(2, 6) );

            Adds(
                new Health(5),
                new Dropable {Items = {new Drop(ITEMS.WOOD_LOG, 1f, 1, 5), new Drop(ITEMS.PINE_CONE, 1f, 0, 3)}}
            );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var offx = -14;
            var offy = -24;
            treeSprite.Draw(spriteBatch, new Vector2(X + offx, Y + offy - 64), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is PlayerEntity || e is ZombieEntity;
        }
    }
}