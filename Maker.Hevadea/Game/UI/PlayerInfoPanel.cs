using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Rise.Extension;
using Maker.Rise.Ressource;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

            for (int i = 0; i < 10 * (playerHealth.Health / playerHealth.MaxHealth); i++)
            {
                hearth.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y, 32, 32), Color.White);
            }

            for (int i = 0; i < 10 * (playerEnergy.Energy / playerEnergy.MaxEnergy); i++)
            {
                energy.Draw(spriteBatch, new Rectangle(Bound.X + 32 * i, Bound.Y + 32, 32, 32), Color.White);
            }


        }

        protected override void OnUpdate(GameTime gameTime)
        {
        }
    }
}