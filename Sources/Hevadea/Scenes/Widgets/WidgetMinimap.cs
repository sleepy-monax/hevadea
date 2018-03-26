using Hevadea.Framework.UI;
using Hevadea.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetMinimap : Widget
    {
        private GameManager _game;
        
        public WidgetMinimap(GameManager game)
        {
            _game = game;
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_game?.MainPlayer?.Level?.Map != null)
            {
                var map = _game.MainPlayer.Level.Map;
                var p = _game.MainPlayer.GetTilePosition();
                spriteBatch.Draw(map, Host, new Rectangle(p.X - 32, p.Y - 32, 64, 64), Color.White);
            }
            
            GuiHelper.DrawBox(spriteBatch, Host, Scale(64));
        }
    }
}