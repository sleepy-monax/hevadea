using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public class EntityTorch : Entity
    {
        private readonly Sprite _sprite;

        public EntityTorch()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(4, 0));

            AddComponent(new Light { On = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72 });
            AddComponent(new Dropable { Items = { new Drop(ITEMS.TORCH, 1f, 1, 1) } });
            AddComponent(new Breakable());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 7, Y - 14), Color.White);
        }
    }
}