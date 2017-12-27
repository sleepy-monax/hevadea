using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            spriteBatch.FillRectangle(this.Bound, Color.Black * 0.25f);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
