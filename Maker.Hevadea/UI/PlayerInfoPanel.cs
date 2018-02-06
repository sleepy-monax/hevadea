using Maker.Hevadea.Game.Entities;
using Maker.Rise.Ressource;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Rise.UI.Widgets;

namespace Maker.Hevadea.Game.UI
{
    public class PlayerInfoPanel : Widget
    {
        private PlayerEntity Player;
        private Sprite hearth;
        private Sprite energy;

        public PlayerInfoPanel(PlayerEntity player)
        {
            Player = player;
            hearth = new Sprite(Ressources.tile_icons, 0);
            energy = new Sprite(Ressources.tile_icons, 1);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var playerHealth = Player.Components.Get<Health>();
            var playerEnergy = Player.Components.Get<Energy>();

            var health = playerHealth.GetValue();
            var energyV = (playerEnergy.Value / playerEnergy.MaxValue);


            //spriteBatch.FillRectangle(Bound, Color.Black * 0.5f);

            var i = 0;

            for (i = 0; i <= 10 * health - 1; i++)
            {
                hearth.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y, 32, 32), Color.White);
            }

            hearth.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y, 32, 32), Color.White * (float)(10 * health - Math.Floor(10 * health)));

            for (i = 0; i <= 10 * energyV - 1; i++)
            {
                energy.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y + 32, 32, 32), Color.White);
            }

            energy.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y + 32, 32, 32), Color.White * (float)( 10 * energyV - Math.Floor(10 * energyV)));
        }
    }
}