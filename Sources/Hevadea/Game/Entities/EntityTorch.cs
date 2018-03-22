using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityTorch : Entity
    {
        private readonly Sprite _sprite;

        public EntityTorch()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(4, 0));
            Attachs(
                new Light {On = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72},
                new Dropable {Items = {new Drop(ITEMS.TORCH, 1f, 1, 1)}},
                new Breakable()
            );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 7, Y - 14), Color.White);
        }
    }
}