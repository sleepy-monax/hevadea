using System;
using System.Security.Cryptography.X509Certificates;
using Hevadea.Framework.UI;
using Hevadea.Framework.Utils;
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

            if (_game?.MainPlayer?.Level?.Minimap?.Texture != null)
            {
                var map = _game.MainPlayer.Level.Minimap.Texture;
                var p = _game.MainPlayer.GetTilePosition();
                
                var offset = new Point(p.X - UnitHost.Width / 4 / 2, p.Y - UnitHost.Height / 4 / 2);  
                var rect = new Point(UnitHost.Width / 4, UnitHost.Height / 4);
                var src = new Rectangle(offset, rect);
       
                spriteBatch.Draw(map, Host, src, Color.White);
                spriteBatch.Draw(Ressources.ImgMapOver, Bound, Color.White);
                
                foreach (var w in _game.MainPlayer.Level.Minimap.Waypoints)
                {
                    Ressources.MinimapIcon[w.Icon].Draw(spriteBatch, new Vector2(Host.X + Mathf.Clamp(Scale((w.X - offset.X) * 4 - 16), 0, Host.Width - Scale(16)),
                                                                                 Host.Y + Mathf.Clamp(Scale((w.Y - offset.Y) * 4 - 16), 0, Host.Height - Scale(16))), new Vector2(Scale(4)), Color.White);
                }
            }

        }
    }
}