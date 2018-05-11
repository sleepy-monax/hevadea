using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Blueprints.Legacy
{
    public class EntityTree : Entity
    {
        private readonly Sprite treeSprite;

        public EntityTree()
        {
            treeSprite = new Sprite(Ressources.TileEntities, new Point(6, 4), new Point(2, 6));

            AddComponent(new Dropable { Items = { new Drop(ITEMS.WOOD_LOG, 1f, 1, 5), new Drop(ITEMS.PINE_CONE, 1f, 0, 3) } });
            AddComponent(new Health(5));
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Burnable(0.5f));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            treeSprite.Draw(spriteBatch, new Vector2(X - 16, Y - 84), Color.White);
        }
    }
}