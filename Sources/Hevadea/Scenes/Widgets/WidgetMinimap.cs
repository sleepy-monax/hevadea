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

            spriteBatch.Draw(Ressources.ImgMap, Bound, Color.White);

            if (_game?.MainPlayer?.Level?.Map != null)
            {
                var map = _game.MainPlayer.Level.Map;
                var p = _game.MainPlayer.GetTilePosition();
                var src = new Rectangle(p.X - UnitHost.Width / 4 / 2, p.Y - UnitHost.Width / 4 / 2, UnitHost.Width / 4,
                    UnitHost.Height / 4);
                spriteBatch.Draw(map, Host, src, Color.White);
            }

            spriteBatch.Draw(Ressources.ImgMapOver, Bound, Color.White);
        }
    }
}