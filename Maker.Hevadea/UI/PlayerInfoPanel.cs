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
            var playerHealth = Player.Components.Get<Health>();
            var playerEnergy = Player.Components.Get<Energy>();

            var health = (playerHealth.Value / playerHealth.MaxValue);
            var energyV = (playerEnergy.Value / playerEnergy.MaxValue);


            spriteBatch.FillRectangle(Bound, Color.Black * 0.5f);

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

            var HoldingItem = Player.HoldingItem;
            if (HoldingItem != null)
            {
                HoldingItem.GetSprite().Draw(spriteBatch, new Rectangle(Bound.X + 320 + 16, Bound.Y + 8, 48, 48), Color.White);
                spriteBatch.DrawString(Ressources.fontRomulus, HoldingItem.GetName(), new Vector2(Bound.X + 320 + 16 + 48 + 8, Bound.Y + 8), Color.White);
            }

        }

        protected override void OnUpdate(GameTime gameTime)
        {
        }
    }
}