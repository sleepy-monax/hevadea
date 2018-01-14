using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class TorchEntity : Entity
    {
        Sprite sprite;

        public TorchEntity()
        {
            Height = 2;
            Width = 2;

            sprite = new Sprite(Ressources.tile_entities, 0, new Point(16, 16));
            AddComponent(new LightComponent { On = true, Color = Color.White, Power = 72 });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.DrawSubSprite(spriteBatch, new Vector2(X - 7, Y - 14), new Point(1, 1), Color.White);
        }
    }
}