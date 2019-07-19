using Hevadea.Entities;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Hevadea.Entities.Components;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetPlayerStats : Widget
    {
        private readonly Sprite _energy;
        private readonly Sprite _hearth;
        private readonly Player _player;

        public WidgetPlayerStats(Player player)
        {
            _player = player;
            _hearth = new Sprite(Resources.TileIcons, 0);
            _energy = new Sprite(Resources.TileIcons, 1);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_player.HasComponent<ComponentEnergy>() && !_player.HasComponent<ComponentHealth>()) return;

            var health = _player.GetComponent<ComponentHealth>().Value;
            var energy = _player.GetComponent<ComponentEnergy>().ValuePercent;
            
            var i = 0;
            var size = Scale(48);
            for (i = 0; i <= health - 1; i++)
                _hearth.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y, size, size), Color.White);

            _hearth.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y, size, size),
                Color.White * (float) (health - Math.Floor(health)));

            for (i = 0; i <= 10 * energy - 1; i++)
                _energy.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y + size, size, size), Color.White);

            _energy.Draw(spriteBatch, new Rectangle(Bound.X + size * i, Bound.Y + size, size, size),
                Color.White * (float) (10 * energy - Math.Floor(10 * energy)));
        }
    }
}