using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class EntityTree : Entity
    {
        private readonly Sprite treeSprite;

        public EntityTree()
        {
            treeSprite = new Sprite(Ressources.TileEntities, new Point(6, 4), new Point(2, 6));
            AddComponent(new Flammable());

            AddComponent(new Collider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.WOOD_LOG, 1f, 1, 5), new Drop(ITEMS.PINE_CONE, 1f, 0, 3) } });
            AddComponent(new Health(5));
            AddComponent(new CastShadow() { Scale = 1.5f });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            treeSprite.Draw(spriteBatch, new Vector2(X - 16, Y - 84), Color.White);
        }
    }
}