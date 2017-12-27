using System;
using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise.GameComponent;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game.UI
{
    public class PlayerInfoPanel : Control
    {
        private Player Player;

        public PlayerInfoPanel(UiManager ui, Player player) : base(ui, false)
        {
            Player = player;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
