using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hevadea.Registry;

namespace Hevadea.Entities
{
    public class Torch : Entity
    {
        private readonly Sprite _sprite;

        public Torch()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(4, 0));

            AddComponent(new Breakable());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.TORCH, 1f, 1, 1) } });
            AddComponent(new LightSource { IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72 });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 7, Y - 14), Color.White);
        }
    }
}