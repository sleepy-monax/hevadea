using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetPlayerStats : Widget
    {
        private readonly Sprite _energy;
        private readonly Sprite _hearth;
        private readonly EntityPlayer _player;

        public WidgetPlayerStats(EntityPlayer player)
        {
            _player = player;
            _hearth = new Sprite(Ressources.TileIcons, 0);
            _energy = new Sprite(Ressources.TileIcons, 1);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_player.HasComponent<Energy>() && !_player.HasComponent<Health>()) return;

            var health = _player.GetComponent<Health>().ValuePercent;
            var energy = _player.GetComponent<Energy>().ValuePercent;

            var i = 0;
            var size = Scale(64);
            for (i = 0; i <= 10 * health - 1; i++)
                _hearth.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y, size, size), Color.White);

            _hearth.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y, size, size),
                Color.White * (float)(10 * health - Math.Floor(10 * health)));

            for (i = 0; i <= 10 * energy - 1; i++)
                _energy.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y + size, size, size), Color.White);

            _energy.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y + size, size, size),
                Color.White * (float)(10 * energy - Math.Floor(10 * energy)));
        }
    }
}