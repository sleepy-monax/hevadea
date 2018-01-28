using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Registry;
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
            Components.Adds(
                new Light { On = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72 },
                new Dropable { Items = { (ITEMS.TORCH_ITEM, 1, 1) } },
                new Breakable()
                );
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.DrawSubSprite(spriteBatch, new Vector2(X - 7, Y - 14), new Point(1, 3), Color.White);
        }
    }
}