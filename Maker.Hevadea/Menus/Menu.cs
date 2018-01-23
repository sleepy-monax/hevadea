using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Menus
{
    public class Menu : Control
    {
        public bool PauseGame = false;
        public GameManager Game;

        public Menu(GameManager game)
        {
            Game = game;
            Padding = new Padding(8);
        }

        public void Show()
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }

        public void Close()
        {

        }
    }
}