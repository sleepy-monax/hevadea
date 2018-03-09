using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityTree : Entity
    {
        private readonly Sprite treeSprite;

        public EntityTree()
        {
            treeSprite = new Sprite(Ressources.TileEntities, new Point(6, 4), new Point(2, 6) );

            Attach(new Dropable { Items = { new Drop(ITEMS.WOOD_LOG, 1f, 1, 5), new Drop(ITEMS.PINE_CONE, 1f, 0, 3) } });
            Attach(new Health(5));
            Attach(new Colider(new Rectangle(-2, -2, 4, 4)));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var offx = -16;
            var offy = -24;
            treeSprite.Draw(spriteBatch, new Vector2(X + offx, Y + offy - 64), Color.White);
        }
    }
}