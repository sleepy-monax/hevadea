using Hevadea.Game.Entities.Component;
using Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class TorchEntity : Entity
    {
        private readonly Sprite _sprite;

        public TorchEntity()
        {
            Height = 2;
            Width = 2;

            _sprite = new Sprite(Ressources.TileEntities, new Point(4, 0));
            Components.Adds(
                new Light {On = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72},
                new Dropable {Items = {(ITEMS.TORCH, 1, 1)}},
                new Breakable()
            );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 7, Y - 14), Color.White);
        }
    }
}