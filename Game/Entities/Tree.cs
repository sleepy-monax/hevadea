using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic;
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
            treeSprite = new Sprite(Resources.TileEntities, new Point(6, 4), new Point(2, 6));
            AddComponent(new ComponentFlammable());

            AddComponent(new ComponentCollider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new ComponentDropable
                {Items = {new Drop(ITEMS.MATERIAL_WOOD_LOG, 1f, 1, 5), new Drop(ITEMS.PINE_CONE, 1f, 0, 3)}});
            AddComponent(new ComponentHealth(5));
            AddComponent(new ComponentCastShadow() {Scale = 1.5f});
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            treeSprite.Draw(spriteBatch, new Vector2(X - 16, Y - 84), Color.White);
        }
    }
}