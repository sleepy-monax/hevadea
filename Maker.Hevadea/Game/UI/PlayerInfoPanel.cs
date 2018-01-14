using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Rise.Extension;
using Maker.Rise.Ressource;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.UI
{
    public class PlayerInfoPanel : Control
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

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var playerHealth = Player.GetComponent<HealthComponent>();
            var playerEnergy = Player.GetComponent<EnergyComponent>();

            spriteBatch.FillRectangle(new Rectangle(Bound.X - 4, Bound.Y, 320 + 8, 64), Color.Black * 0.2f);

            var health = (playerHealth.Health / playerHealth.MaxHealth);
            var energyV = (playerEnergy.Energy / playerEnergy.MaxEnergy);


            int i = 0;

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

        protected override void OnUpdate(GameTime gameTime)
        {
        }
    }
}