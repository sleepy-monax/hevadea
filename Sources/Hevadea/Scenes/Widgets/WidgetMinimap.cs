using System;
using System.Security.Cryptography.X509Certificates;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetMinimap : Widget
    {
        private GameManager.GameManager _game;
        
        public WidgetMinimap(GameManager.GameManager game)
        {
            _game = game;
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Host, Color.Black * 0.5f);

            if (_game?.MainPlayer?.Level?.Minimap?.Texture != null)
            {
                var states = _game.MainPlayer.Level.GetRenderState(_game.Camera);
                var map = _game.MainPlayer.Level.Minimap.Texture;
                var p = _game.MainPlayer.GetTilePosition().ToPoint();
                
                var dest = UnitHost;
                var offset = new Point(p.X - dest.Width / 4 / 2, p.Y - dest.Height / 4 / 2);  
                var rect = new Point(dest.Width / 4, dest.Height / 4);
                var src = new Rectangle(offset, rect);
       
                spriteBatch.Draw(map, Host, src, Color.White);

                foreach (var w in _game.MainPlayer.Level.Minimap.Waypoints)
                {
                    Ressources.MinimapIcon[w.Icon].Draw(spriteBatch, new Vector2(Host.X + Mathf.Clamp(Scale((w.X - offset.X) * 4 - 16), 0, Host.Width - Scale(16)),
                                                                                 Host.Y + Mathf.Clamp(Scale((w.Y - offset.Y) * 4 - 16), 0, Host.Height - Scale(16))), new Vector2(Scale(4)), Color.White);
                }
                
                spriteBatch.PutPixel((Host.Location + Scale((p - offset) * new Point(4))).ToVector2(), Color.Blue, Scale(4));
                spriteBatch.DrawRectangle(Host.Location + Scale((states.Begin - offset) * new Point(4)) , Host.Location + Scale((states.End - offset) * new Point(4)), Scale(4f), Color.Red * 0.5f);
            }
        }
    }
}