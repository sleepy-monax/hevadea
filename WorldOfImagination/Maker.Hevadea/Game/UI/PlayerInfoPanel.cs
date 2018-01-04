using Maker.Rise.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Hevadea.Game.Entities;

namespace Maker.Hevadea.Game.UI
{
    public class PlayerInfoPanel : Control
    {
        private Player Player;

        public PlayerInfoPanel(Player player) : base(false)
        {
            Player = player;
            
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(this.Bound, Color.Black * 0.25f);
            spriteBatch.DrawString(Ressources.font_alagard, $"Healh: {Player.Health}/{Player.MaxHealth}", new Vector2(16,16), Color.White);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            
        }
    }
}
