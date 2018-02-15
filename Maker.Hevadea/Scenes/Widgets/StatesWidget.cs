using System;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Rise.Ressource;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes.Widgets
{
    public class StatesWidget : Widget
    {
        private readonly Sprite _energy;
        private readonly Sprite _hearth;
        private readonly PlayerEntity _player;

        public StatesWidget(PlayerEntity player)
        {
            _player = player;
            _hearth = new Sprite(Ressources.TileIcons, 0);
            _energy = new Sprite(Ressources.TileIcons, 1);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var health = _player.Components.Get<Health>().ValuePercent;
            var energy = _player.Components.Get<Energy>().ValuePercent;

            var i = 0;

            for (i = 0; i <= 10 * health - 1; i++)
                _hearth.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y, 32, 32), Color.White);

            _hearth.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y, 32, 32),
                Color.White * (float) (10 * health - Math.Floor(10 * health)));

            for (i = 0; i <= 10 * energy - 1; i++)
                _energy.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y + 32, 32, 32), Color.White);

            _energy.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y + 32, 32, 32),
                Color.White * (float) (10 * energy - Math.Floor(10 * energy)));
        }
    }
}